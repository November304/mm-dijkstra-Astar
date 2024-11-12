using System.Collections.Generic;
using UnityEngine;

public class AlgoDijsktra : AlgoPathfinding
{
    private Dictionary<Vector2Int, float> distances;
    private NodeAbs currentNode;

    public AlgoDijsktra(TileState[,] grid, Vector2Int start, Vector2Int end)
        : base(grid, start, end)
    {
        // Initialize step-by-step state variables
        openSet = new PriorityQueue<NodeAbs>();
        distances = new Dictionary<Vector2Int, float>();

        // Add the starting node
        NodeDijsktra startNode = new(start, 0);
        openSet.Enqueue(startNode, 0);
        distances[start] = 0;

        // Initialize visited set and current node
        visited = new HashSet<Vector2Int>();
    }

    public override List<Vector2Int>? FindPath()
    {
        while (openSet.Count > 0)
        {
            StepInfo? step = Step();
            if (step == null)
            {
                return null;
            }
            if (step.Value.Position == end)
            {
                return ReconstructPath(currentNode);
            }
        }

        return null;
    }

    public override StepInfo? Step()
    {
        if (openSet.Count == 0)
            return null;

        // Dequeue the node with the smallest distance
        currentNode = openSet.Dequeue();

        // If we have already visited this node, skip it
        if (visited.Contains(currentNode.Position))
            return new StepInfo(currentNode.Position, currentNode.Distance, false, 0, true);

        visited.Add(currentNode.Position);

        // Process neighbors
        foreach (Vector2Int neighbor in GetNeighbors(currentNode.Position))
        {
            if (visited.Contains(neighbor) || grid[neighbor.x, neighbor.y] == TileState.WALL)
                continue;

            float newDist = currentNode.Distance + 1; // Assume uniform cost for Dijsktra

            if (!distances.ContainsKey(neighbor) || newDist < distances[neighbor])
            {
                distances[neighbor] = newDist;
                NodeDijsktra neighborNode = new(neighbor, newDist)
                {
                    Previous = currentNode
                };
                openSet.Enqueue(neighborNode, newDist);
                allNodes[neighbor] = neighborNode;
            }
        }

        // Return step information
        return new StepInfo(
            currentNode.Position,
            currentNode.Distance,
            currentNode.Position == end,
            0, // No heuristic in Dijsktra
            true
        );
    }
}
