
using UnityEngine;

public class Node
{
    public Vector2Int Position;
    public float Distance;
    public Node Previous;

    public Node(Vector2Int position, float distance)
    {
        Position = position;
        Distance = distance;
        Previous = null;
    }
}