using UnityEngine;

public class NodeAStar : NodeAbs
{
    public float F => Distance + Heuristic; // F = G + H
    public NodeAStar(Vector2Int position, float distance, float heuristic) : base(position, distance, heuristic)
    {
    }

    

}
