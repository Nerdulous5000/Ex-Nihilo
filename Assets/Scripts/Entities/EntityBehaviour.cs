using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityBehaviour : MonoBehaviour {

    public uint Id { get; private set; }
    public Tile Tile { get; protected set; }
    public Vector2Int Position { get; protected set; }

    public Sprite Sprite { get { return data.Sprite; } }
    public int Width { get { return data.Width; } }
    public int Height { get { return data.Height; } }
    public ItemInterface Interface { get { return data.Interface; } }
    public virtual string TooltipString {
        get {
            return "Id: " + Id + "\nWidth: " + Width + "\nHeight: " + Height;
        }
    }
    [SerializeField]
    protected EntityData data;
    static uint idCount = 0;
    // EntityManager manager;

    public static EntityBehaviour Initialize(EntityData entityData, Vector2Int position) {

        EntityBehaviour instance = Instantiate<EntityBehaviour>(Resources.Load<EntityBehaviour>("Entities/BaseEntity"));
        instance.Id = AssignId();
        instance.data = entityData;
        instance.Position = position;
        Tile tile = Tile.CreateInstance<Tile>();
        tile.sprite = instance.Sprite;
        instance.Tile = tile;
        return instance;
    }
    static uint AssignId() {
        return idCount++;
    }

    public virtual void OnSpawn() {
        Debug.Log("Entity spawned");
    }

    public virtual void OnUse() {
        Debug.Log("Entity did the thing");
    }

    public virtual void OnKill() {
        Debug.Log("Entity has died");
    }

    bool Give<T>(T item)
    {
        return false;
    }
    bool Recieve<T>(T item)
    {
        return false;
    }
    public bool IsAdjacent(Vector2Int location) {
        bool adj = (
            // Vertical bounds
            ((location.x >= Position.x && location.x < Position.x + Width) ||
            // Horizontal bounds 
            (location.y >= Position.y && location.y < Position.y + Height)) &&
            // 1 unit away
            ((location.x >= Position.x -1   && location.x <= Position.x + Width) && (location.y >= Position.y - 1 && location.y <= Position.y + Height))
        );
        return adj;
    }

}
