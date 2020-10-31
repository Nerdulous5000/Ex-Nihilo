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

    public static Direction RotateCW(this Direction dir) {
        switch (dir) {
            case Direction.Right:
                return Direction.Down;
            case Direction.Up:
                return Direction.Right;
            case Direction.Left:
                return Direction.Up;
            case Direction.Down:
                return Direction.Left;
            default:
                return Direction.Null;
        }
    }

    public static Direction RotateCCW(this Direction dir) {
        switch (dir) {
            case Direction.Right:
                return Direction.Up;
            case Direction.Up:
                return Direction.Left;
            case Direction.Left:
                return Direction.Down;
            case Direction.Down:
                return Direction.Right;
            default:
                return Direction.Null;
        }
    }

    // If diagonal, favors vertical directions
    // If zero vector, returns up
    public static Direction FromVector2(Vector2 vec) {
        if (vec.x <= vec.y) {
            return vec.x >= 0 ? Direction.Up : Direction.Down;
        } else {
            return vec.x >= 0 ? Direction.Right : Direction.Left;
        }
    }

    // If diagonal, favors vertical directions
    // If zero vector, returns up
    public static Direction FromVector2Int(Vector2Int vec) {
        if (vec.x <= vec.y) {
            return vec.x >= 0 ? Direction.Up : Direction.Down;
        } else {
            return vec.x >= 0 ? Direction.Right : Direction.Left;
        }
    }


}
