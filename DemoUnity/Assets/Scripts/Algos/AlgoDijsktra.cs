using System.Collections.Generic;
using UnityEngine;

public class AlgoDijsktra : AlgoPathfinding
{
    private List<NodeDijsktra> openList;
    private Dictionary<Vector2Int, float> distances;
    private HashSet<Vector2Int> visited;
    private NodeDijsktra currentNode;

    public AlgoDijsktra(TileState[,] grid, Vector2Int start, Vector2Int end) : base(grid, start, end)
    {
        // Initialize the step-by-step state variables
        openList = new List<NodeDijsktra>();
        distances = new Dictionary<Vector2Int, float>();
        visited = new HashSet<Vector2Int>();

        // Start with the starting node
        NodeDijsktra startNode = new(start, 0);
        openList.Add(startNode);
        distances[start] = 0;
    }

    public override List<Vector2Int> FindPath()
    {
        List<Vector2Int> fullPath = new();

        while (true)
        {
            Vector2Int? result = StepFindPath();
            if (result == null)
                break;
            fullPath.Add(result.Value);
        }

        return fullPath.Count > 0 ? ReconstructPath(currentNode) : null;
    }

    public Vector2Int? StepFindPath()
    {
        if (openList.Count == 0)
        {
            return null;
        }

        // Find the node with the minimal distance
        openList.Sort((a, b) => a.Distance.CompareTo(b.Distance));
        currentNode = openList[0];
        openList.RemoveAt(0);

        // If we reached the end node, return null to signal completion
        if (currentNode.Position == end)
        {
            return null;
        }

        if (visited.Contains(currentNode.Position))
        {
            return StepFindPath();
        }

        visited.Add(currentNode.Position);

        foreach (Vector2Int neighbor in GetNeighbors(currentNode.Position))
        {
            if (visited.Contains(neighbor) || grid[neighbor.x, neighbor.y] == TileState.WALL)
            {
                continue;
            }

            float newDist = distances[currentNode.Position] + 1;

            if (!distances.ContainsKey(neighbor) || newDist < distances[neighbor])
            {
                distances[neighbor] = newDist;
                NodeDijsktra neighborNode = new(neighbor, newDist)
                {
                    Previous = currentNode
                };
                openList.Add(neighborNode);
            }
        }

        return currentNode.Position;
    }

    
}
