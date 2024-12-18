using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private Vector2Int mapSize = new(0, 0);
    private float wallChance;

    public static Generator Instance;

    private TileState[,] grid;
    private bool isGenerated = false;

    [Header("Tiles")]
    [SerializeField] private Tile normalTile;
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile startTile;
    [SerializeField] private Tile endTile;
    [SerializeField] private Tile pathTile;
    [SerializeField] private Tile visitedTile;

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// G�n�re une map al�atoire
    /// </summary>
    /// <param name="seed">Le seed sur lequel bas� la map</param>
    /// <param name="size">La taille de la map</param>
    /// <param name="wallChance">La chance d'avoir un mur</param>
    public void Generate(int seed, Vector2Int size, float wallChance)
    {
        isGenerated = true;
        Random.InitState(seed);
        mapSize = size;
        this.wallChance = wallChance;
        grid = new TileState[size.x, size.y];

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                grid[x, y] = TileState.NORMAL;
            }
        }
        GenerateWalls();
        RenderGrid();
    }

    /// <summary>
    /// G�n�ration al�atoire des murs
    /// </summary>
    private void GenerateWalls()
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                if (Random.Range(0f, 1f) < wallChance)
                {
                    grid[x, y] = TileState.WALL;
                }
            }
        }
    }

    /// <summary>
    /// Affiche la grid dans le tilemap
    /// </summary>
    /// <param name="grid">La grid a afficher</param>
    public void RenderGrid()
    {
        tilemap.ClearAllTiles();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                SetPos(new Vector2Int(x, y), grid[x, y]);
            }
        }
    }

    /// <summary>
    /// Retourne la taille de la map
    /// </summary>
    /// <returns>Le vecteur d'entier pour la taille de la map</returns>
    public Vector2Int GetMapSize()
    {
        return mapSize;
    }

    /// <summary>
    /// Renvoie la grid actuelle
    /// </summary>
    /// <returns>Le tableau a 2 dimensions</returns>
    public TileState[,] GetGrid()
    {
        return grid;
    }

    /// <summary>
    /// Set une position dans la grid
    /// </summary>
    /// <param name="pos">La position a set dans la grid</param>
    /// <param name="state">L'etat a mettre dans la grid</param>
    public void SetPos(Vector2Int pos, TileState state)
    {
        grid[pos.x, pos.y] = state;
        switch (state)
        {
            case TileState.NORMAL:
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), normalTile);
                break;
            case TileState.WALL:
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), wallTile);
                break;
            case TileState.START:
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), startTile);
                break;
            case TileState.END:
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), endTile);
                break;
            case TileState.PATH:
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), pathTile);
                break;
            case TileState.VISITED:
                tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), visitedTile);
                break;

        }
    }

    /// <summary>
    /// Set une position dans la grid si possible
    /// </summary>
    /// <param name="pos">La position a set dans la grid</param>
    /// <param name="state">Le nouveau state a mettre</param>
    public void TrySetPos(Vector2Int pos, TileState state)
    {
        if (!isGenerated || pos.x < 0 || pos.x >= mapSize.x || pos.y < 0 || pos.y >= mapSize.y)
        {
            return;
        }
        SetPos(pos, state);

    }

    /// <summary>
    /// Set le chemin dans la grid
    /// </summary>
    /// <param name="chemin">La liste des positions du chemin</param>
    public void SetChemin(Vector2Int[] chemin)
    {
        foreach (Vector2Int pos in chemin)
        {
            SetPos(pos, TileState.PATH);
        }
    }

    /// <summary>
    /// Convertit une position dans le monde en position dans le tilemap
    /// </summary>
    /// <param name="worldPos">La position dans le monde a convertir</param>
    /// <returns>Un vector2Int</returns>
    public Vector2Int WorldToTilemapPos(Vector3 worldPos)
    {
        Vector3Int cellPos = tilemap.WorldToCell(worldPos);
        return new Vector2Int(cellPos.x, cellPos.y);
    }

    /// <summary>
    /// Est ce que la map a �t� g�n�r�e
    /// </summary>
    /// <returns>True si oui, false sinon</returns>
    public bool IsGenerated()
    {
        return isGenerated;
    }
}
