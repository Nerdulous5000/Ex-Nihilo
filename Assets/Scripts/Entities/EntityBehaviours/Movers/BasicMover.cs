using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Entities.Mover {
    public class BasicMover : Mover {

        [SerializeField]
        protected float moveDelay;
        Timer timer;


        void Start() {
            timer = new Timer();
            timer.Set(moveDelay);
            timer.OnFinish(MoveItem);
            timer.Repeat = true;
        }

        public override void OnSpawn() {
            Inventory = new Inventory(1);
        }

        void FixedUpdate() {
            timer.Tick();
        }

        void MoveItem() {

            if(PullItem(Direction.Up))
                Debug.Log("Pulled Item.");
            if(GiveItem(Direction.Right))
                Debug.Log("Gave Item.");
        }

    }
}