using UnityEngine;

public interface IInputHandler
{
    Vector2 Direction { get; }
    bool IsDragging { get; }
}
