using System.Collections.Generic;
using UnityEngine;

#nullable enable
public abstract class AlgoPathfinding
{
    protected TileState[,] grid;
    protected Vector2Int start;
    protected Vector2Int end;
    protected int width;
    protected int height;

    protected HashSet<Vector2Int> visited;
    protected PriorityQueue<NodeAbs> openSet;
    protected Dictionary<Vector2Int, NodeAbs> allNodes;

    public AlgoPathfinding(TileState[,] grid, Vector2Int start, Vector2Int end)
    {
        this.grid = grid;
        this.start = start;
        this.end = end;
        width = grid.GetLength(0);
        height = grid.GetLength(1);

        visited = new HashSet<Vector2Int>();
        allNodes = new Dictionary<Vector2Int, NodeAbs>();
    }

    /// <summary>
    /// Fully finds the path from start to end.
    /// </summary>
    /// <returns>The list of positions representing the path.</returns>
    public abstract List<Vector2Int>? FindPath();

    /// <summary>
    /// Executes one step of the pathfinding algorithm and provides info about the current step.
    /// </summary>
    /// <returns>StepInfo with details about the current step.</returns>
    public abstract StepInfo? Step();

    /// <summary>
    /// Retrieves neighbors of a given position.
    /// </summary>
    /// <param name="position">The position to find neighbors for.</param>
    /// <returns>A list of valid neighbor positions.</returns>
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

    /// <summary>
    /// Reconstructs the path from the end node back to the start node.
    /// </summary>
    /// <param name="endNode">The end node of the path.</param>
    /// <returns>The reconstructed path as a list of positions.</returns>
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
}



