using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : EntityBehaviour {

    // [SerializeField]
    // protected float moveDelay;
    // Timer timer;


    // void Start() {
    //     timer = new Timer();
    //     timer.Set(moveDelay);
    //     timer.OnFinish(MoveItem);
    //     timer.Repeat = true;
    // }
    protected int moveRadius = 1;

    public override void OnSpawn() {
        Inventory = new Inventory(1);
    }

    // void FixedUpdate() {
    //     timer.Tick();
    // }
    protected bool PullItem(Direction localDirection) {
        Vector2Int pullLocation = Position + (DirectionLocalToGlobal(localDirection).ToVector2Int() * moveRadius);
        if (!EntityManager.Instance.IsNullAt(pullLocation) && Inventory.IsEmpty()) {
            ItemStack heldStack = EntityManager.Instance.At(pullLocation).Inventory.Pull();
            if(heldStack == null) {
                return false;
            }
            return Inventory.Give(heldStack);
        }
        else {
            return false;
        }
    }

    protected bool GiveItem(Direction localDirection) {
        Vector2Int giveLocation = Position + (DirectionLocalToGlobal(localDirection).ToVector2Int() * moveRadius);
        if (!EntityManager.Instance.IsNullAt(giveLocation) && !Inventory.IsEmpty()) {
            ItemStack heldStack = Inventory.Pull();
            return EntityManager.Instance.At(giveLocation).Inventory.Give(heldStack);
        } else {
            return false;
        }
    }

    // void MoveItem() {


    //     if(PullItem(Direction.Up))
    //         Debug.Log("Pulled Item.");
    //     if(GiveItem(Direction.Right))
    //         Debug.Log("Gave Item.");
    // }

}
