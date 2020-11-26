using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : EntityBehaviour {
    // public Item item;
    // public int itemQuantity;
    public float moveDelay;
    Timer timer;

    // [SerializeField]
    // ItemStack heldStack;

    void Start() {
        timer = new Timer();
    }

    public override void OnSpawn() {
        Inventory = new Inventory(1);
    }

    void FixedUpdate() {
        Vector2Int fromLocation = Position + Direction.Up.RotateCCW(Rotation).ToVector2Int();
        Vector2Int toLocation = Position + Direction.Up.RotateCCW(Rotation - 1).ToVector2Int();
        

        if(!EntityManager.Instance.IsNullAt(fromLocation) && Inventory.IsEmpty()) {
            ItemStack heldStack = EntityManager.Instance.At(fromLocation).Inventory.Pull();
            Inventory.Give(heldStack);
            if(heldStack != null)
                Debug.Log("Picked up " + heldStack.Count + " " + heldStack.Item);
        }

        if(!EntityManager.Instance.IsNullAt(toLocation) && !Inventory.IsEmpty()) {

            ItemStack heldStack = Inventory.Pull();

            EntityManager.Instance.At(toLocation).Inventory.Give(heldStack);

            Debug.Log("Gave " + heldStack.Count + " " + heldStack.Item);
        }
        
    }

}
