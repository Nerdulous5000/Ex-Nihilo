using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    Null = -1,
    Right = 0,
    Up = 1,
    Left = 2,
    Down = 3,
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

    public static Direction RotateCW(this Direction dir, int val = 1) {
        if(dir == Direction.Null) {
            return Direction.Null;
        }
        return (Direction)(((int)dir + 4 - val) % 4);
    }

    public static Direction RotateCCW(this Direction dir, int val = 1) {
        if (dir == Direction.Null) {
            return Direction.Null;
        }
        return (Direction)(((int)dir + 4 + val) % 4);
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
