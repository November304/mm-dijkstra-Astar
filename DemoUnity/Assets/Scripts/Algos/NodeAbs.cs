using UnityEngine;

public abstract class NodeAbs
{
    public Vector2Int Position;
    public float Distance; // G, distance du départ=
    public NodeAbs Previous;

    public float Heuristic; // H, heuristique (distance estimée vers la fin)

    public NodeAbs(Vector2Int position, float distance)
    {
        Position = position;
        Distance = distance;
        Previous = null;
    }

    public NodeAbs(Vector2Int position, float distance, float heuristic)
    {
        Position = position;
        Distance = distance;
        Heuristic = heuristic;
        Previous = null;
    }


}
