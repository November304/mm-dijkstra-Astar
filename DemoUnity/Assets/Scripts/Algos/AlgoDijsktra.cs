using System.Collections.Generic;
using UnityEngine;

public class AlgoDijsktra
{
    private TileState[,] grid;
    private Vector2Int start;
    private Vector2Int end;
    private int width;
    private int height;

    public AlgoDijsktra(TileState[,] grid, Vector2Int start, Vector2Int end)
    {
        this.grid = grid;
        this.start = start;
        this.end = end;
        width = grid.GetLength(0);
        height = grid.GetLength(1);
    }

    public List<Vector2Int> FindPath()
    {
        var openList = new List<Node>();
        var startNode = new Node(start, 0);
        openList.Add(startNode);

        var distances = new Dictionary<Vector2Int, float>();
        var visited = new HashSet<Vector2Int>();

        distances[start] = 0;

        while (openList.Count > 0)
        {
            // Trouver le noeud avec la distance minimale dans openList
            openList.Sort((a, b) => a.Distance.CompareTo(b.Distance));
            var currentNode = openList[0];
            openList.RemoveAt(0);

            if (currentNode.Position == end)
            {
                return ReconstructPath(currentNode);
            }

            if (visited.Contains(currentNode.Position))
            {
                continue;
            }

            visited.Add(currentNode.Position);

            foreach (var neighbor in GetNeighbors(currentNode.Position))
            {
                if (visited.Contains(neighbor) || grid[neighbor.x, neighbor.y] == TileState.WALL)
                {
                    continue;
                }

                float newDist = distances[currentNode.Position] + 1;

                if (!distances.ContainsKey(neighbor) || newDist < distances[neighbor])
                {
                    distances[neighbor] = newDist;
                    var neighborNode = new Node(neighbor, newDist);
                    neighborNode.Previous = currentNode;
                    openList.Add(neighborNode);
                }
            }
        }

        return null;
    }

    private List<Vector2Int> GetNeighbors(Vector2Int position)
    {
        var neighbors = new List<Vector2Int>();

        var directions = new Vector2Int[]
        {
            new Vector2Int(0, 1),  // Haut
            new Vector2Int(1, 0),  // Droite
            new Vector2Int(0, -1), // Bas
            new Vector2Int(-1, 0)  // Gauche
        };

        foreach (var direction in directions)
        {
            var neighborPos = position + direction;
            if (neighborPos.x >= 0 && neighborPos.x < width && neighborPos.y >= 0 && neighborPos.y < height)
            {
                neighbors.Add(neighborPos);
            }
        }

        return neighbors;
    }

    private List<Vector2Int> ReconstructPath(Node endNode)
    {
        var path = new List<Vector2Int>();
        var currentNode = endNode;

        while (currentNode != null)
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Previous;
        }

        path.Reverse();
        return path;
    }
}
