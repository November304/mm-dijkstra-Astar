using UnityEngine;

public class NodeAStar
{
    public Vector2Int Position;
    public float Distance; // G, distance du d�part
    public float Heuristic; // H, heuristique (distance estim�e vers la fin)
    public float F => Distance + Heuristic; // F = G + H
    public NodeAStar Previous;

    public NodeAStar(Vector2Int position, float distance, float heuristic)
    {
        Position = position;
        Distance = distance;
        Heuristic = heuristic;
        Previous = null;
    }
}
