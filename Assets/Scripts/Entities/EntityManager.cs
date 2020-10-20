using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EntityManager : MonoBehaviour {
    public static EntityManager Instance;

    // Keeps a reference to entities at each occupied tile (via id) *Useful for multi-tile entities
    Dictionary<Vector2Int, uint> entityIdMap;

    // Keeps a reference of all active entities indexed a unique id
    Dictionary<uint, Entity> entityList;

    [SerializeField]
    Tilemap tilemap = null;

    void Awake() {
        Initialize();
        Instance = this;
    }

    void Start() {
        // Initialize();
    }
    void Update() {

    }
    void Initialize() {
        entityIdMap = new Dictionary<Vector2Int, uint>();
        entityList = new Dictionary<uint, Entity>();
        tilemap.ClearAllTiles();
    }

    // Checks to see if the entity can be placed, then places entity with bool confirmation
    public bool Spawn(string entityName, Vector2Int position) {
        Entity refEntity = EntityTable.Entities[entityName];
        if (!CanSpawn(refEntity, position)) {
            return false;
        }
        Entity entity = Instantiate(refEntity);
        entity.Initialize();
        entity.Position = position;
        for (int y = entity.Position.y; y < entity.Position.y + entity.Height; y++) {
            for (int x = entity.Position.x; x < entity.Position.x + entity.Width; x++) {
                entityIdMap[new Vector2Int(x, y)] = entity.Id;
            }
        }
        entityList[entity.Id] = entity;
        tilemap.SetTile((Vector3Int)entity.Position, entity.Tile);
        return true;
    }

    public bool CanSpawn(Entity entity, Vector2Int position) {

        for (int y = position.y; y < position.y + entity.Height; y++) {
            for (int x = position.x; x < position.x + entity.Width; x++) {
                if (!IsNullAt(new Vector2Int(x, y))) {
                    return false;
                }
            }
        }
        return true;
    }

    public bool Kill(Vector2Int pos) {
        if (IsNullAt(pos)) {
            return false;
        }
        Entity entity = At(pos);

        // Clear tile on tilemap
        tilemap.SetTile((Vector3Int)entity.Position, null);

        // Clear spaces on id map
        for (int y = entity.Position.y; y < entity.Position.y + entity.Height; y++) {
            for (int x = entity.Position.x; x < entity.Position.x + entity.Width; x++) {
                entityIdMap.Remove(new Vector2Int(x, y));
            }
        }
        Destroy(entity.gameObject);
        entityList.Remove(entity.Id);

        return true;
    }

    // Returns entity object referenced to at position
    public Entity At(Vector2Int pos) {
        if (IsNullAt(pos)) {
            return null;
        }
        uint id = entityIdMap[pos];
        return entityList[id];
    }

    // Checks if any entity is occupying specified tile
    public bool IsNullAt(Vector2Int pos) {
        return !entityIdMap.ContainsKey(pos);
    }

    public Entity EntityById(uint id) {
        if (entityList.ContainsKey(id)) {
            return entityList[id];
        } else {
            return null;
        }
    }
}
