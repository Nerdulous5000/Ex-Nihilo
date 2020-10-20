using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Entity : MonoBehaviour {
    public uint Id { get; private set; }
    public int Width;
    public int Height;
    public Tile Tile;
    public Vector2Int Position;
    public Sprite Sprite {
        get {
            return Tile.sprite;
        }
    }
    public string TooltipString {
        get {
            return "Id: " + Id + "\nWidth: " + Width + "\nHeight: " + Height;
        }
    }
    static uint idCount = 0;
    EntityManager manager;

    public void Initialize() {
        Id = AssignId();
    }
    static uint AssignId() {
        return idCount++;
    }
}
