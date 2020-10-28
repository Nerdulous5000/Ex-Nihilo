using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unable to be instantiated
// Must create derivative with defined size 
public abstract class ItemInterface : ScriptableObject {


    public enum IODirection {
        Null = 0,
        Input = 1,
        Output = 2,
    };
    Dictionary<Direction, IODirection[]> IOSlots;

    // Through array index goes from left to right.
    // All other directions are rotated from top *See below

    // Right : Top    ->  Bottom
    // Top   : Left   ->  Right
    // Left  : Bottom ->  Top
    // Down  : Right  ->  Left

    // TODO: Add intuitive GUI to change io

    protected void Initialize(int width, int height) {
        for (int n = 0; n < width; n++) {
            IOSlots[Direction.Up] = new IODirection[width];
            IOSlots[Direction.Down] = new IODirection[width];
        }
        for (int n = 0; n < height; n++) {
            IOSlots[Direction.Left] = new IODirection[height];
            IOSlots[Direction.Right] = new IODirection[height];
        }
    }

    public bool CanAccept(Direction direction, uint index = 0) {
        if (index >= IOSlots[direction].Length) {
            return false;
        }
        if (IOSlots[direction][index] == IODirection.Input) {
            return true;
        } else {
            return false;
        }
    }
    public bool CanGive(Direction direction, uint index = 0) {
        if (index >= IOSlots[direction].Length) {
            return false;
        }
        if (IOSlots[direction][index] == IODirection.Input) {
            return true;
        } else {
            return false;
        }
    }

    public void SetInput(Direction direction, uint index = 0) {
        if (index >= IOSlots[direction].Length) {
            return;
        }
        IOSlots[direction][index] = IODirection.Input;
    }
    public void SetOutput(Direction direction, uint index = 0) {
        if (index >= IOSlots[direction].Length) {
            return;
        }
        IOSlots[direction][index] = IODirection.Output;
    }

    public abstract Vector2Int GetTile(Vector2Int position, Direction direction, uint index);

}


