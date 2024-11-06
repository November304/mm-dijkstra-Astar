
using System.Collections.Generic;
using UnityEngine;

public abstract class Pathfinding
{
    protected TileState[,] grid;
    protected Vector2Int start;
    protected Vector2Int end;
    protected int width;
    protected int height;

    public Pathfinding(TileState[,] grid, Vector2Int start, Vector2Int end)
    {
        this.grid = grid;
        this.start = start;
        this.end = end;
        width = grid.GetLength(0);
        height = grid.GetLength(1);
    }

    public abstract List<Vector2Int> FindPath();
}