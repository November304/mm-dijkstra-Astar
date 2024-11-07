
using System.Collections.Generic;
using UnityEngine;

public abstract class AlgoPathfinding
{
    protected TileState[,] grid;
    protected Vector2Int start;
    protected Vector2Int end;
    protected int width;
    protected int height;

    public AlgoPathfinding(TileState[,] grid, Vector2Int start, Vector2Int end)
    {
        this.grid = grid;
        this.start = start;
        this.end = end;
        width = grid.GetLength(0);
        height = grid.GetLength(1);
    }

    public abstract List<Vector2Int> FindPath();

    protected List<Vector2Int> ReconstructPath(NodeAbs endNode)
    {
        List<Vector2Int> path = new();
        NodeAbs currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Previous;
        }

        path.Reverse();
        return path;
    }

    /// <summary>
    /// Renvoie la liste des positions voisines valides de la position donnée
    /// </summary>
    /// <param name="position">La position dont on veut les voisins</param>
    /// <returns>La liste des voisings</returns>
    protected List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        List<Vector2Int> neighbors = new();

        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // Up
            new Vector2Int(1, 0),  // Right
            new Vector2Int(0, -1), // Down
            new Vector2Int(-1, 0)  // Left
        };

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborPos = position + direction;
            if (neighborPos.x >= 0 && neighborPos.x < width && neighborPos.y >= 0 && neighborPos.y < height)
            {
                neighbors.Add(neighborPos);
            }
        }

        return neighbors;
    }
}