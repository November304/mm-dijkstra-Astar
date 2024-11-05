using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    [SerializeField] private Vector2Int size;

    private TileState[,] grid;

    [SerializeField] private Tile normalTile;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile startTile;
    [SerializeField] private Tile endTile;
    [SerializeField] private Tile pathTile;

    private Vector2Int startPos;
    private Vector2Int endPos;

    public void Generate()
    {
        grid = new TileState[size.x, size.y];

        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                grid[x, y] = TileState.NORMAL;
            }
        }

        //TODO : Generate walls

        //TODO : Faire positions randoms
        startPos = new Vector2Int(0, 0);
        endPos = new Vector2Int(size.x - 1, size.y - 1);

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
                }
            }
        }

        tilemap.SetTile(new Vector3Int(startPos.x, startPos.y, 0), startTile);
        tilemap.SetTile(new Vector3Int(endPos.x, endPos.y, 0), endTile);

    }

    private void Start()
    {
        Generate();
        RenderGrid();
    }
}
