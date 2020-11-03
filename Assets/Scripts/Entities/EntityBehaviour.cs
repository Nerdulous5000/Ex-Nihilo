using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityBehaviour : MonoBehaviour {

    public uint Id { get; private set; }
    public Tile Tile { get; protected set; }
    public Vector2Int Position { get; protected set; }

    public Sprite Sprite;// { get { return data.Sprite; } }
    public int Width;// { get { return data.Width; } }
    public int Height;// { get { return data.Height; } }
    public ItemInterface Interface;// { get { return data.Interface; } }
    public virtual string TooltipString {
        get {
            return "Id: " + Id + "\nWidth: " + Width + "\nHeight: " + Height;
        }
    }

    // TODO: Determine mandatory inventory on all entitybehaviours

    [SerializeField]
    protected Dictionary<Item, int> inventory = new Dictionary<Item, int>();


    // [SerializeField]
    // protected EntityData data;
    static uint idCount = 0;
    // EntityManager manager;

    public void Initialize(Vector2Int position) {
        // EntityBehaviour instance = Instantiate<EntityBehaviour>(EntityTable.Entities[entityName]);
        Id = AssignId();
        // instance.data = entityData;
        Position = position;
        Tile tile = Tile.CreateInstance<Tile>();
        tile.sprite = Sprite;
        Tile = tile;
        return;
    }
    static uint AssignId() {
        return idCount++;
    }

    public virtual void OnSpawn() {
        // Debug.Log("Entity spawned");
    }

    public virtual void OnUse() {
        // Debug.Log("Entity did the thing");
    }

    public virtual void OnKill() {
        // Debug.Log("Entity has died");
    }

    protected bool Give(Item item, Direction direction, int index = 0, int quantity = 1) {
        if (quantity <= 0) {
            return false;
        }
        if (!Interface.CanGive(direction, index)) {
            return false;
        }
        Vector2Int otherEntityLocation = Interface.DirIndexToVector2Int(Position, direction, index);
        EntityBehaviour other = EntityManager.Instance.At(otherEntityLocation);
        if (other == null) {
            return false;
        }
        if (other.Interface == null) {
            return false;
        }
        if (inventory[item] < quantity) {
            return false;
        }
        if (!inventory.ContainsKey(item)) {
            return false;
        }
        if (other.Recieve(item, direction.Opposite(), index, quantity)) {

            inventory[item] -= quantity;
            return true;
        }
        return false;
    }
    bool Recieve(Item item, Direction direction, int index = 0, int quantity = 1) {
        if (quantity <= 0) {
            return false;
        }
        if (!Interface.CanRecieve(direction, index)) {
            return false;
        }
        // add the thing
        if(!inventory.ContainsKey(item)) {
            inventory[item] = 0;
        }
        inventory[item] += quantity;
        Debug.Log(inventory[item]);
        return true;
    }
    public bool IsAdjacent(Vector2Int location) {
        bool adj = (
            (
                // Horizontal bounds
                (location.IsRightFrom(Position + new Vector2Int(-1, 0)) && location.IsLeftFrom(Position + new Vector2Int(Width, 0)))
                ||
                // Vertical bounds 
                (location.IsUpFrom(Position + new Vector2Int(0, -1)) && location.IsDownFrom(Position + new Vector2Int(0, Height)))
            )
            &&
            // 1 unit away
            (
                location.IsRightFrom(Position + new Vector2Int(-1, 0)) &&
                location.IsUpFrom(Position + new Vector2Int(0, -1)) &&
                location.IsLeftFrom(Position + new Vector2Int(Width, 0)) &&
                location.IsDownFrom(Position + new Vector2Int(0, Height))
            )
        );
        return adj;
    }



}
