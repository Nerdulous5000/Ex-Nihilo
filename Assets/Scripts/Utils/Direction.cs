using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Null,
    Right,
    Up,
    Left,
    Down,
}

static class DirectionExtensions {
    public static Direction Opposite(this Direction dir) {
        switch (dir) {
            case Direction.Right:
                return Direction.Left;
            case Direction.Up:
                return Direction.Down;
            case Direction.Left:
                return Direction.Right;
            case Direction.Down:
                return Direction.Up;
            default:
                return Direction.Null;
        }
    }
}
