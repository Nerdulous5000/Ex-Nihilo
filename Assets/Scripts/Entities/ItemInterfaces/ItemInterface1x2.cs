﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInterface", menuName = "Gameplay/ItemInterfaces/1x2")] 
public class ItemInterface1x2 : ItemInterface{
    // [] []

    [Header("Right")]
    public IODirection right0;
    [Header("Up")]
    public IODirection up0;
    public IODirection up1;
    [Header("Left")]
    public IODirection left0;
    [Header("Down")]
    public IODirection down0;
    public IODirection down1;

    void OnEnable()
    {
        Initialize(2, 1);
        // Right
        if (right0 == IODirection.Input) {
            SetInput(Direction.Right, 0);
        } else if (right0 == IODirection.Output) {
            SetOutput(Direction.Right, 0);
        }
        // Up
        if (up0 == IODirection.Input) {
            SetInput(Direction.Up, 0);
        } else if (up0 == IODirection.Output) {
            SetOutput(Direction.Up, 0);
        }
        if (up1 == IODirection.Input) {
            SetInput(Direction.Up, 1);
        } else if (up1 == IODirection.Output) {
            SetOutput(Direction.Up, 1);
        }
        // Left
        if (left0 == IODirection.Input) {
            SetInput(Direction.Left, 0);
        } else if (left0 == IODirection.Output) {
            SetOutput(Direction.Left, 0);
        }
        // Down
        if (down0 == IODirection.Input) {
            SetInput(Direction.Down, 0);
        } else if (down0 == IODirection.Output) {
            SetOutput(Direction.Down, 0);
        }
        if (down1 == IODirection.Input) {
            SetInput(Direction.Down, 1);
        } else if (down1 == IODirection.Output) {
            SetOutput(Direction.Down, 1);
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