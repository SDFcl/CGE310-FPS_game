using UnityEngine;

public interface IPlayerActivity
{
    Vector2 MoveInput { get; }
    Vector2 LookInput { get; }
    bool IsAction { get; }
}
