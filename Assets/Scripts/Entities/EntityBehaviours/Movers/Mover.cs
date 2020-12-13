using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Entities.Mover {
    public class Mover : EntityBehaviour {

        [SerializeField]
        [Range(0, 1)]
        public float Progress = 0;
        // protected float moveDelay;
        // Timer timer;
        protected Timer pullTimer = new Timer();
        protected Timer pushTimer = new Timer();


        protected enum MoveDirection {
            CounterClockwise = 0,
            Clockwise = 1,
        };
        protected MoveState currentMoveDirection;

        protected enum MoveState {
            Giving = 0,
            Pulling = 1,
        };
        protected MoveState currentMoveState;

        protected int moveRadius = 1;



        public override void OnSpawn() {
            Inventory = new Inventory(1);
            CanGiveItems = false;
        }

        protected bool PullItem(Direction localDirection) {
            Vector2Int pullLocation = Position + (DirectionLocalToGlobal(localDirection).ToVector2Int() * moveRadius);
            if (!EntityManager.Instance.IsNullAt(pullLocation) && Inventory.IsEmpty()) {
                if(EntityManager.Instance.At(pullLocation).CanAcceptItems) {
                    ItemStack heldStack = EntityManager.Instance.At(pullLocation).Inventory.Pull();
                    if (heldStack == null) {
                        return false;
                    }
                    return Inventory.Give(heldStack);
                }
                else {
                    return false;
                }

            } else {
                return false;
            }
        }

        protected bool GiveItem(Direction localDirection) {
            Vector2Int giveLocation = Position + (DirectionLocalToGlobal(localDirection).ToVector2Int() * moveRadius);
            if (!EntityManager.Instance.IsNullAt(giveLocation) && !Inventory.IsEmpty()) {
                if(EntityManager.Instance.At(giveLocation).CanAcceptItems) {
                    ItemStack heldStack = Inventory.Pull();
                    return EntityManager.Instance.At(giveLocation).Inventory.Give(heldStack);
                }
                else {
                    return false;
                }
            } else {
                return false;
            }
        }

        protected void UpdateRotation() {

            Vector3 handPos = Quaternion.Euler(0, 0, -90 * Progress) * Vector2.up * (1 + moveRadius);
            // GameObject arm = GameObject.Find("Arm");
            // arm.transform.rotation = Quaternion.Euler(0, 0, -90.0f * Progress);
            Debug.DrawRay(new Vector3(Position.x, Position.y, 0), handPos, Color.magenta);
        }

        void Update() {
            UpdateRotation();
            Progress = Mathf.Sin(Time.frameCount / 50);
        }
        // void MoveItem() {


        //     if(PullItem(Direction.Up))
        //         Debug.Log("Pulled Item.");
        //     if(GiveItem(Direction.Right))
        //         Debug.Log("Gave Item.");
        // }

    }
}
