using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInterface", menuName = "Gameplay/ItemInterfaces/2x1")] 
public class ItemInterface2x1 : ItemInterface{
    // []
    // []

    [Header("Right")]
    public IODirection right0;
    public IODirection right1;
    [Header("Up")]
    public IODirection up0;
    [Header("Left")]
    public IODirection left0;
    public IODirection left1;
    [Header("Down")]
    public IODirection down0;

    void OnEnable()
    {
        Initialize(1, 2);
        // Right
        if (right0 == IODirection.Input) {
            SetInput(Direction.Right, 0);
        } else if (right0 == IODirection.Output) {
            SetOutput(Direction.Right, 0);
        }
        if (right1 == IODirection.Input) {
            SetInput(Direction.Right, 1);
        } else if (right1 == IODirection.Output) {
            SetOutput(Direction.Right, 1);
        }
        // Up
        if (up0 == IODirection.Input) {
            SetInput(Direction.Up, 0);
        } else if (up0 == IODirection.Output) {
            SetOutput(Direction.Up, 0);
        }
        // Left
        if (left0 == IODirection.Input) {
            SetInput(Direction.Left, 0);
        } else if (left0 == IODirection.Output) {
            SetOutput(Direction.Left, 0);
        }
        if (left1 == IODirection.Input) {
            SetInput(Direction.Left, 1);
        } else if (left1 == IODirection.Output) {
            SetOutput(Direction.Left, 1);
        }
        // Down
        if (down0 == IODirection.Input) {
            SetInput(Direction.Down, 0);
        } else if (down0 == IODirection.Output) {
            SetOutput(Direction.Down, 0);
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
