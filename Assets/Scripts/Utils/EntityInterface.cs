using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Used to manage I/O of items for an entity 
public class EntityInterface {
    // in
    // out
    public Dictionary<Direction, bool> InSlots { get; private set; }
    public Dictionary<Direction, bool> OutSlots { get; private set; }
    public EntityInterface() {
        InSlots[Direction.Right] = false;
        OutSlots[Direction.Right] = false;

        InSlots[Direction.Up] = false;
        OutSlots[Direction.Up] = false;

        InSlots[Direction.Left] = false;
        OutSlots[Direction.Left] = false;

        InSlots[Direction.Down] = false;
        OutSlots[Direction.Down] = false;
    }

    public void SetInput(Direction direction, bool val) {
        InSlots[direction] = val;
    }
    public void EnableInput(Direction direction) {
        InSlots[direction] = true;
    }
    public void DisableInput(Direction direction) {
        InSlots[direction] = false;
    }
    public void SetOutput(Direction direction, bool val) {
        OutSlots[direction] = val;
    }
    public void EnableOutput(Direction direction) {
        OutSlots[direction] = true;
    }
    public void DisableOutput(Direction direction) {
        OutSlots[direction] = false;
    }
    public bool InputEnabled(Direction dir) {
        return InSlots[dir];
    }
    public bool OutputEnabled(Direction dir) {
        return OutSlots[dir];
    }
}
