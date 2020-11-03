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
    Dictionary<Direction, IODirection[]> IOSlots = new Dictionary<Direction, IODirection[]>();

    public int Width;
    public int Height;

    // Through array index goes from left to right.
    // All other directions are rotated from top *See below

    // Right : Top    ->  Bottom
    // Top   : Left   ->  Right
    // Left  : Bottom ->  Top
    // Down  : Right  ->  Left

    public List<IODirection> right = new List<IODirection>();
    public List<IODirection> up = new List<IODirection>();
    public List<IODirection> left = new List<IODirection>();
    public List<IODirection> down = new List<IODirection>();


    // TODO: Add intuitive GUI to change io


    void OnEnable() {
        Initialize();
    }

    protected void Initialize() {
        IOSlots[Direction.Right] = new IODirection[Height];
        IOSlots[Direction.Up] = new IODirection[Width];
        IOSlots[Direction.Left] = new IODirection[Height];
        IOSlots[Direction.Down] = new IODirection[Width];
        if (right.Count > 0) {
            for (int n = 0; n < right.Count; n++) {
                IOSlots[Direction.Right][n] = right[n];
            }
        }
        if (up.Count > 0) {
            for (int n = 0; n < up.Count; n++) {
                IOSlots[Direction.Up][n] = up[n];
            }
        }
        if (left.Count > 0) {
            for (int n = 0; n < left.Count; n++) {
                IOSlots[Direction.Left][n] = left[n];
            }
        }
        if (down.Count > 0) {
            for (int n = 0; n < down.Count; n++) {
                IOSlots[Direction.Down][n] = down[n];
            }
        }
    }

    public bool CanRecieve(Direction direction, int index = 0) {
        if (index >= IOSlots[direction].Length) {
            return false;
        }
        if (IOSlots[direction][index] == IODirection.Input) {
            return true;
        } else {
            return false;
        }
    }
    public bool CanGive(Direction direction, int index = 0) {
        if (index >= IOSlots[direction].Length) {
            return false;
        }
        if (IOSlots[direction][index] == IODirection.Output) {
            return true;
        } else {
            return false;
        }
    }

    public void SetInput(Direction direction, int index = 0) {
        if (index >= IOSlots[direction].Length) {
            return;
        }
        IOSlots[direction][index] = IODirection.Input;
    }
    public void SetOutput(Direction direction, int index = 0) {
        if (index >= IOSlots[direction].Length) {
            return;
        }
        IOSlots[direction][index] = IODirection.Output;
    }
    public void Clear(Direction direction, int index = 0) {
        if (index >= IOSlots[direction].Length) {
            return;
        }
        IOSlots[direction][index] = IODirection.Null;
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

    // Returns position of tile  given the parameters direction and index relative to a specified position
    public Vector2Int DirIndexToVector2Int(Vector2Int position, Direction direction, int index = 0) {
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

    // Assumes destination tile is adjacent to interface (no diagonals)
    public (Direction direction, int index) Vector2IntToDirIndex(Vector2Int position, Vector2Int location) {
        // Gets direction
        Direction outDir;
        // Up/Down
        if (location.IsRightFrom(position + new Vector2Int(-1, 0)) && location.IsLeftFrom(position + new Vector2Int(Width, 0))) {
            outDir = location.IsUpFrom(position) ? Direction.Up : Direction.Down;
        }
        // Left/Right
        else if (location.IsUpFrom(position + new Vector2Int(0, -1)) && location.IsDownFrom(position + new Vector2Int(Height + 1, 0))) {
            outDir = location.IsRightFrom(position) ? Direction.Right : Direction.Left;
        }
        // Default
        else {
            outDir = Direction.Null;
        }

        // Gets index
        int outIndex = 0;
        switch (outDir) {
            case Direction.Right:
                outIndex = (position.y + Height - 1 - location.y);
                break;
            case Direction.Up:
                outIndex = (location.x - position.x);
                break;
            case Direction.Down:
                outIndex = (position.x + Width - 1 - location.x);
                break;
            case Direction.Left:
                outIndex = (location.y - position.y);
                break;
            default:
                break;
        }
        return (outDir, outIndex);
    }


}


