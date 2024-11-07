using System.Collections.Generic;
using UnityEngine;

public class AlgoAStar : AlgoPathfinding
{
    public AlgoAStar(TileState[,] grid, Vector2Int start, Vector2Int end) : base(grid, start, end)
    {
    }

    public override List<Vector2Int> FindPath()
    {
        List<NodeAStar> openList = new();
        NodeAStar startNode = new(start, 0, Heuristic(start, end));
        openList.Add(startNode);

        Dictionary<Vector2Int, float> distances = new();
        HashSet<Vector2Int> visited = new();

        distances[start] = 0;

        while (openList.Count > 0)
        {
            // Tri de openList en fonction de la somme coût + heuristique (f = g + h)
            openList.Sort((a, b) => a.F.CompareTo(b.F));
            NodeAStar currentNode = openList[0];
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
                    NodeAStar neighborNode = new(neighbor, newDist, Heuristic(neighbor, end));
                    neighborNode.Previous = currentNode;
                    openList.Add(neighborNode);
                }
            }
        }

        return null; // Pas de chemin trouvé
    }

    private float Heuristic(Vector2Int a, Vector2Int b)
    {
        // Distance de Manhattan pour une grille
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

}
