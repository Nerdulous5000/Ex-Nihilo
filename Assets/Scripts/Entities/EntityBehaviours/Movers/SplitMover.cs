using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Mover {
    public class SplitMover : Mover {

        [SerializeField]
        protected float moveDelay;
        Timer timer;
        GiveDirection givingDirection;

        enum GiveDirection {
            Left = 0,
            Right = 1,
        };


        void Start() {
            timer = new Timer();
            timer.Set(moveDelay);
            timer.OnFinish(MoveItem);
            timer.Repeat = true;
            moveRadius = 2;
        }

        public override void OnSpawn() {
            Inventory = new Inventory(1);
        }

        void FixedUpdate() {
            timer.Tick();
        }

        void MoveItem() {

            if (PullItem(Direction.Up))
                Debug.Log("Pulled Item.");
            bool given;
            switch(givingDirection) {
                case GiveDirection.Left:
                    given = GiveItem(Direction.Left);
                    break;
                case GiveDirection.Right:
                    given = GiveItem(Direction.Right);
                    break;
                default:
                    given = GiveItem(Direction.Right);
                    break;
            }
            if (given) {
                Debug.Log("Gave Item.");
                ToggleSide();
            }
        }

        void ToggleSide() {
            switch(givingDirection) {
                case GiveDirection.Left:
                    givingDirection = GiveDirection.Right;
                    break;
                case GiveDirection.Right:
                    givingDirection = GiveDirection.Left;
                    break;
                default:
                    givingDirection = GiveDirection.Right;
                    break;
            }
        }
    }
}
