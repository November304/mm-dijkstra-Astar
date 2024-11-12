using System.Collections.Generic;
using UnityEngine;

#nullable enable
public class AlgoAStar : AlgoPathfinding
{
    private Dictionary<Vector2Int, float> distances;
    private NodeAbs currentNode;

    public AlgoAStar(TileState[,] grid, Vector2Int start, Vector2Int end)
        : base(grid, start, end)
    {
        // Initialisation des structures de donn�es
        openSet = new PriorityQueue<NodeAbs>();
        distances = new Dictionary<Vector2Int, float>();

        // Ajouter le n�ud de d�part
        NodeAStar startNode = new(start, 0, Heuristic(start, end));
        openSet.Enqueue(startNode, startNode.F);
        distances[start] = 0;

        // Initialiser les n�uds visit�s
        visited = new HashSet<Vector2Int>();
    }

    public override List<Vector2Int>? FindPath()
    {
        // Parcourir les �tapes de l'algorithme jusqu'� trouver un chemin ou �puiser les possibilit�s
        while (openSet.Count > 0)
        {
            StepInfo? step = Step();
            if(step == null)
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

        // R�cup�rer le n�ud avec la plus basse valeur de f (f = g + h)
        currentNode = openSet.Dequeue();

        if (visited.Contains(currentNode.Position))
            return new StepInfo(currentNode.Position, currentNode.Distance, false, ((NodeAStar)currentNode).Heuristic, true);

        visited.Add(currentNode.Position);

        // Traiter les voisins
        foreach (Vector2Int neighbor in GetNeighbors(currentNode.Position))
        {
            if (visited.Contains(neighbor) || grid[neighbor.x, neighbor.y] == TileState.WALL)
                continue;

            float newDist = currentNode.Distance + 1; // Co�t uniforme pour le d�placement
            float heuristic = Heuristic(neighbor, end);

            if (!distances.ContainsKey(neighbor) || newDist < distances[neighbor])
            {
                distances[neighbor] = newDist;
                NodeAStar neighborNode = new(neighbor, newDist, heuristic)
                {
                    Previous = currentNode
                };
                openSet.Enqueue(neighborNode, neighborNode.F);
                allNodes[neighbor] = neighborNode;
            }
        }

        // Retourner les informations de l'�tape
        return new StepInfo(
            currentNode.Position,
            currentNode.Distance,
            currentNode.Position == end,
            ((NodeAStar)currentNode).Heuristic,
            true
        );
    }

    private float Heuristic(Vector2Int a, Vector2Int b)
    {
        // Distance de Manhattan pour une grille
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
}
