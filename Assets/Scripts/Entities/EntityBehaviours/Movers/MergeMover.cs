using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeMover : Mover {

    [SerializeField]
    protected float moveDelay;
    Timer timer;
    PullDirection pullingDirection;

    enum PullDirection {
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

        bool pulled;
        switch (pullingDirection) {
            case PullDirection.Left:
                given = PullItem(Direction.Left);
                break;
            case PullDirection.Right:
                given = PullItem(Direction.Right);
                break;
            default:
                given = PullItem(Direction.Right);
                break;
        }
        if (given) {
            Debug.Log("Pulled Item.");
            ToggleSide();
        }


        if (GiveItem(Direction.Up))
            Debug.Log("Gave Item.");
    }

    void ToggleSide() {
        switch (pullingDirection) {
            case PullDirection.Left:
                pullingDirection = PullDirection.Right;
                break;
            case PullDirection.Right:
                pullingDirection = PullDirection.Left;
                break;
            default:
                pullingDirection = PullDirection.Right;
                break;
        }
    }

}
