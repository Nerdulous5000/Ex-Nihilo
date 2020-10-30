using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Unable to be instantiated
// Must create derivative with defined size 

[CreateAssetMenu(fileName = "ItemInterface", menuName = "Gameplay/ItemInterface")]
public class ItemInterface : ScriptableObject {

    public enum IODirection {
        Null = 0,
        Input = 1,
        Output = 2,
    };
    Dictionary<Direction, IODirection[]> IOSlots;

    public int Width;
    public int Height;

    // Through array index goes from left to right.
    // All other directions are rotated from top *See below

    // Right : Top    ->  Bottom
    // Top   : Left   ->  Right
    // Left  : Bottom ->  Top
    // Down  : Right  ->  Left

    public List<IODirection> right;
    public List<IODirection> up;
    public List<IODirection> left;
    public List<IODirection> down;


    // TODO: Add intuitive GUI to change io


    void OnEnable() {
        if (right == null || up == null || left == null || down == null) {
            Initialize(Width, Height);
        }
    }

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
    public void Clear(Direction direction, uint index = 0) {
        if (index >= IOSlots[direction].Length) {
            return;
        }
        IOSlots[direction][index] = IODirection.Null;
    }

    // Returns position of tile  given the parameters direction and index relative to a specified position
    public Vector2Int GetTile(Vector2Int position, Direction direction, int index = 0) {
        if ((direction == Direction.Right || direction == Direction.Left) && index >= Height) {
            return new Vector2Int();
        }
        if ((direction == Direction.Up || direction == Direction.Down) && index >= Width) {
            return new Vector2Int();
        }
        switch (direction) {
            case Direction.Right:
                return position + new Vector2Int(Width, Height - 1 - index);
            case Direction.Up:
                return position + new Vector2Int(index, Height);
            case Direction.Left:
                return position + new Vector2Int(-1, index);
            case Direction.Down:
                return position + new Vector2Int(Width - 1 - index, -1);
            default:
                return new Vector2Int();
        }
    }

    // Returns a list of all relative locations available to be interacted with 
    public List<Vector2Int> GetValidTiles() {
        List<Vector2Int> ret = new List<Vector2Int>();
        // Right
        for (int index = 0; index < Height; index++) {
            ret.Add(new Vector2Int(Width, Height - 1 - index));
        }
        // Up
        for (int index = 0; index < Width; index++) {
            ret.Add(new Vector2Int(index, Height));
        }
        // Left
        for (int index = 0; index < Height; index++) {
            ret.Add(new Vector2Int(-1, index));
        }
        // Down
        for (int index = 0; index < Width; index++) {
            ret.Add(new Vector2Int(Width - 1 - index, -1));
        }
        return ret;
    }

    // Returns the relative locations of all inputs
    public List<Vector2Int> GetInputs() {
        List<Vector2Int> ret = new List<Vector2Int>();
        // Right
        for (int index = 0; index < Height; index++) {
            if (right[index] == IODirection.Input) {
                ret.Add(new Vector2Int(Width, Height - 1 - index));
            }
        }
        // Up
        for (int index = 0; index < Width; index++) {
            if (up[index] == IODirection.Input) {
                ret.Add(new Vector2Int(index, Height));
            }
        }
        // Left
        for (int index = 0; index < Height; index++) {
            if (left[index] == IODirection.Input) {
                ret.Add(new Vector2Int(-1, index));
            }
        }
        // Down
        for (int index = 0; index < Width; index++) {
            if (down[index] == IODirection.Input) {
                ret.Add(new Vector2Int(Width - 1 - index, -1));
            }
        }
        return ret;
    }

    // Returns the relative locations of all outputs
    public List<Vector2Int> GetOutputs() {
        List<Vector2Int> ret = new List<Vector2Int>();
        // Right
        for (int index = 0; index < Height; index++) {
            if (right[index] == IODirection.Output) {
                ret.Add(new Vector2Int(Width, Height - 1 - index));
            }
        }
        // Up
        for (int index = 0; index < Width; index++) {
            if (up[index] == IODirection.Output) {
                ret.Add(new Vector2Int(index, Height));
            }
        }
        // Left
        for (int index = 0; index < Height; index++) {
            if (left[index] == IODirection.Output) {
                ret.Add(new Vector2Int(-1, index));
            }
        }
        // Down
        for (int index = 0; index < Width; index++) {
            if (down[index] == IODirection.Output) {
                ret.Add(new Vector2Int(Width - 1 - index, -1));
            }
        }
        return ret;
    }

}


