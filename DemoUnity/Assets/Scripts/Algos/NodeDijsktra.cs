
using UnityEngine;

public class NodeDijsktra
{
    public Vector2Int Position;
    public float Distance;
    public NodeDijsktra Previous;

    public NodeDijsktra(Vector2Int position, float distance)
    {
        Position = position;
        Distance = distance;
        Previous = null;
    }
}