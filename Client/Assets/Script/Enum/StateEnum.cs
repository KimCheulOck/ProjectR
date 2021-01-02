public enum StateType
{
    None = -1,
    Idle = 0,
    Move,
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    Dead,
    Attack,
    Defense,
    Casting,
    Max = 9,
}

public enum BodyType
{
    Low = 0,
    Up = 1,
    Max = 2,
}

public enum DirectionType
{
    None = 0,
    Left = 1,
    Right = 2,
    Up = 3,
    Down = 4,
}