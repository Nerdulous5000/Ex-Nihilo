using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : EntityBehaviour {
    public Item item;
    public int itemQuantity;
    public float itemDelay;
    Timer timer;
    void Start() {
        timer  = new Timer();
        inventory[item] = itemQuantity;
        timer.Set(itemDelay);
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(timer.IsDone) {
            timer.Set(itemDelay);
            if(Give(item, Direction.Up)) {
                Debug.Log("Gave item!");
            }
        }
        timer.Tick();
    }
}
