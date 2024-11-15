using UnityEngine;
/// <summary>
/// Represents information about a single step of the pathfinding process.
/// </summary>
public struct StepInfo
{
    public Vector2Int Position; // The current position being processed
    public float Distance;      // Distance from the start
    public bool IsInPath;       // Whether this position is in the final path
    public float Heuristic;     // Heuristic value (for A* only)
    public bool IsVisited;      // Whether this tile has been visited

    public StepInfo(Vector2Int position, float distance, bool isInPath, float heuristic, bool isVisited)
    {
        Position = position;
        Distance = distance;
        IsInPath = isInPath;
        Heuristic = heuristic;
        IsVisited = isVisited;
    }
}