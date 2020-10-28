using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInterface", menuName = "Gameplay/ItemInterfaces/1x1")]
public class ItemInterface1x1 : ItemInterface {
    // []

    [Header("Right")]
    public IODirection right;
    [Header("Up")]
    public IODirection up;
    [Header("Left")]
    public IODirection left;
    [Header("Down")]
    public IODirection down;

    void OnEnable() {
        Initialize(1, 1);
        // Right
        if (right == IODirection.Input) {
            SetInput(Direction.Right);
        } else if (right == IODirection.Output) {
            SetOutput(Direction.Right);
        }
        // Up
        if (up == IODirection.Input) {
            SetInput(Direction.Up);
        } else if (up == IODirection.Output) {
            SetOutput(Direction.Up);
        }
        // Left
        if (left == IODirection.Input) {
            SetInput(Direction.Left);
        } else if (left == IODirection.Output) {
            SetOutput(Direction.Left);
        }
        // Down
        if (down == IODirection.Input) {
            SetInput(Direction.Down);
        } else if (down == IODirection.Output) {
            SetOutput(Direction.Down);
        }
    }
    public override Vector2Int GetTile(Vector2Int position, Direction direction, uint index) {
        switch (direction) {
            case Direction.Right:
                return position + new Vector2Int(1, 0);
            case Direction.Up:
                ;
                return position + new Vector2Int(0, 1);
            case Direction.Left:
                ;
                return position + new Vector2Int(-1, 0);
            case Direction.Down:
                ;
                return position + new Vector2Int(0, -1);
            default:
                return new Vector2Int();
        }
    }

}
