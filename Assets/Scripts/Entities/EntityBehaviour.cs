﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Entities {
    public class EntityBehaviour : MonoBehaviour {

        static uint idCount = 0;
        public uint Id { get; private set; }
        public Tile Tile { get; protected set; }
        public Vector2Int Position { get; protected set; }
        public (Vector2Int Min, Vector2Int Max) Extents {
            get {
                Vector3 offset = Quaternion.Euler(0, 0, 90 * Rotation) * new Vector2(Width - 1, Height - 1);
                Vector2Int farPos = Position + new Vector2Int(Mathf.RoundToInt(offset.x), Mathf.RoundToInt(offset.y));
                int minX = Position.x < farPos.x ? Position.x : farPos.x;
                int maxX = Position.x > farPos.x ? Position.x : farPos.x;
                int minY = Position.y < farPos.y ? Position.y : farPos.y;
                int maxY = Position.y > farPos.y ? Position.y : farPos.y;
                return (new Vector2Int(minX, minY), new Vector2Int(maxX, maxY));
            }
        }
        public int Rotation { get; protected set; }

        public Sprite Sprite;// { get { return data.Sprite; } }
        public int Width;// { get { return data.Width; } }
        public int Height;// { get { return data.Height; } }
        public Inventory Inventory { get; protected set; }
        public virtual string TooltipString {
            get {
                return "Id: " + Id + "\nWidth: " + Width + "\nHeight: " + Height;
            }
        }

        public bool CanAcceptItems { get; protected set; } = true;
        public bool CanGiveItems { get; protected set; } = true;




        public void Initialize(Vector2Int position, int rotation) {
            Id = AssignId();
            Position = position;
            Rotation = rotation;
            Tile tile = Tile.CreateInstance<Tile>();
            tile.sprite = Sprite;
            Tile = tile;
        }
        static uint AssignId() {
            return idCount++;
        }

        public virtual void OnSpawn() {
            // Debug.Log("Entity spawned");
        }

        public virtual void OnUse() {
            // Debug.Log("Entity did the thing");
            Debug.Log("Inventory contains: \n" + Inventory.ToString());
        }

        public virtual void OnKill() {
            // Debug.Log("Entity has died");
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

        protected Direction DirectionLocalToGlobal(Direction dir) {
            return dir.RotateCCW(Rotation);
        }
        protected Direction DirectionGlobalToLocal(Direction dir) {
            return dir.RotateCW(Rotation);
        }

    }
}