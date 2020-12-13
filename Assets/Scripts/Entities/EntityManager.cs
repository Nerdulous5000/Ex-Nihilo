using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Entities {
    public class EntityManager : MonoBehaviour {
        public static EntityManager Instance;

        // Keeps a reference to entities at each occupied tile (via id) *Useful for multi-tile entities
        Dictionary<Vector2Int, uint> entityIdMap;

        // Keeps a reference of all active entities indexed a unique id
        Dictionary<uint, EntityBehaviour> entityList;

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
            entityList = new Dictionary<uint, EntityBehaviour>();
            tilemap.ClearAllTiles();
        }


        // Checks to see if the entity can be placed, then places entity with bool confirmation
        public bool Spawn(string entityName, Vector2Int position) {
            if(!EntityTable.Entities.ContainsKey(entityName)) {
                return false;
            }
            EntityBehaviour entity = Instantiate(EntityTable.Entities[entityName]);

            int rotationAmount = SelectionManager.Instance.SelecetedRotation;

            entity.Initialize(position, rotationAmount);
            if (!CanSpawn(entity)) {
                Destroy(entity.gameObject);
                return false;
            }

            PlaceInTileMap(entity);
            PlaceInIdMap(entity);
            PlaceInIdList(entity);

            entity.OnSpawn();

            return true;
        }

        void PlaceInTileMap(EntityBehaviour entity) {
            entity.Tile.transform *= Matrix4x4.Rotate(Quaternion.Euler(0, 0, 90 * entity.Rotation));
            tilemap.SetTile((Vector3Int)entity.Position, entity.Tile);
        }
        void PlaceInIdMap(EntityBehaviour entity) {

            for (int y = entity.Extents.Min.y; y <= entity.Extents.Max.y; y++) {
                for (int x = entity.Extents.Min.x; x <= entity.Extents.Max.x; x++) {
                    entityIdMap[new Vector2Int(x, y)] = entity.Id;
                }
            }
        }
        void PlaceInIdList(EntityBehaviour entity) {
            entityList[entity.Id] = entity;
        }

        public bool CanSpawn(EntityBehaviour entity) {

            for (int y = entity.Extents.Min.y; y <= entity.Extents.Max.y; y++) {
                for (int x = entity.Extents.Min.x; x <= entity.Extents.Max.x; x++) {
                    // entityIdMap[new Vector2Int(x, y)] = entity.Id;
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
            EntityBehaviour entity = At(pos);

            RemoveFromTileMap(entity);
            RemoveFromIdMap(entity);
            RemoveFromIdList(entity);

            entity.OnKill();
            Destroy(entity.gameObject);
            return true;
        }
        void RemoveFromTileMap(EntityBehaviour entity) {
            tilemap.SetTile((Vector3Int)entity.Position, null);
        }
        void RemoveFromIdMap(EntityBehaviour entity) {
            for (int y = entity.Extents.Min.y; y <= entity.Extents.Max.y; y++) {
                for (int x = entity.Extents.Min.x; x <= entity.Extents.Max.x; x++) {
                    entityIdMap.Remove(new Vector2Int(x, y));
                }
            }
        }
        void RemoveFromIdList(EntityBehaviour entity) {
            entityList.Remove(entity.Id);
        }

        // Returns entity object referenced to at position
        public EntityBehaviour At(Vector2Int pos) {
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

        public EntityBehaviour EntityById(uint id) {
            if (entityList.ContainsKey(id)) {
                return entityList[id];
            } else {
                return null;
            }
        }
    }
}
