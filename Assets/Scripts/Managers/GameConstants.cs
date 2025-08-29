using UnityEngine;

public static class Tags
{
    public static readonly string Player = "Player";
    public static readonly string GameController = "GameController";
}

public static class AnimParams
{
    public static readonly int MoveHash = Animator.StringToHash("Move");
    public static readonly int DieHash = Animator.StringToHash("Death");
}

public static class InputActions
{
    public static readonly string vAxis = "Vertical";
    public static readonly string hAxis = "Horizontal";
    public static readonly string fireBtn = "Fire1";
}