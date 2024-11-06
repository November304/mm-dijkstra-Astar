using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private Vector2Int size;

    [SerializeField] private int mapSeed;

    private TileState[,] grid;

    [Header("Tiles")]
    [SerializeField] private Tile normalTile;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile startTile;
    [SerializeField] private Tile endTile;
    [SerializeField] private Tile pathTile;

    private Vector2Int startPos;
    private Vector2Int endPos;

    public void Generate()
    {
        Random.InitState(mapSeed);
        grid = new TileState[size.x, size.y];

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                grid[x, y] = TileState.NORMAL;
            }
        }
        // Générer les murs aléatoires
        GenerateWalls();

        // Générer des positions aléatoires pour le départ et la fin
        SetRandomStartEndPositions();


        AlgoDijsktra algo = new(grid,startPos, endPos);
        var path = algo.FindPath();

        // Afficher le chemin
        if (path != null)
        {
            Debug.Log("Path found with " + path.Count + " steps");  
            foreach (var position in path)
            {
                grid[position.x, position.y] = TileState.PATH;
            }
        }
        else
        {
            Debug.LogWarning("No path found");
        }

        grid[startPos.x, startPos.y] = TileState.START;
        grid[endPos.x, endPos.y] = TileState.END;
    }

    private void GenerateWalls()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if (Random.Range(0,10) < 3)
                {
                    grid[x, y] = TileState.WALL;
                }
            }
        }
    }

    private void SetRandomStartEndPositions()
    {
        do
        {
            startPos = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
        } while (grid[startPos.x, startPos.y] == TileState.WALL);

        do
        {
            endPos = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
        } while (endPos == startPos || grid[endPos.x, endPos.y] == TileState.WALL);

    }

    public void RenderGrid()
    {
        for (int x = 0;x < size.x;x++)
        {
              for (int y = 0;y < size.y;y++)
            {
                switch (grid[x, y])
                {
                    case TileState.NORMAL:
                        tilemap.SetTile(new Vector3Int(x, y, 0), normalTile);
                        break;
                    case TileState.WALL:
                        tilemap.SetTile(new Vector3Int(x, y, 0), wallTile);
                        break;
                    case TileState.START:
                        tilemap.SetTile(new Vector3Int(x, y, 0), startTile);
                        break;
                    case TileState.END:
                        tilemap.SetTile(new Vector3Int(x, y, 0), endTile);
                        break;
                    case TileState.PATH:
                        tilemap.SetTile(new Vector3Int(x, y, 0), pathTile);
                        break;
                    
                }
            }
        }
    }

    private void Start()
    {
        Generate();
        RenderGrid();
    }
}
