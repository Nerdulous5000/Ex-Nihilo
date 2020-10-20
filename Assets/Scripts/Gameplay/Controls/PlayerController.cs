using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float MoveSpeed;
    public float SprintModifier = 1;


    float speedModifier = 1;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector3 moveVec = new Vector3(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0
            );

        speedModifier = Input.GetButton("Sprint") ? SprintModifier : 1;
        transform.position += moveVec * MoveSpeed * speedModifier * Time.deltaTime;
    }
}
