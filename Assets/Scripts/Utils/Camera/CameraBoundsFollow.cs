using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundsFollow : MonoBehaviour {

    public Camera MainCamera;
    public GameObject Target;

    [Range(0.0f, 1.0f)]
    public float FollowThreshold;

    [SerializeField]
    bool debug = false;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Bounds followBounds = GetWorldBounds();
        if (!followBounds.Contains(Target.transform.position)) {
            Vector3 moveVec = Target.transform.position - followBounds.ClosestPoint(Target.transform.position);
            MainCamera.transform.position += moveVec;
        }
    }

    void OnDrawGizmos() {
        if (debug) {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(MainCamera.transform.position, new Vector3(MainCamera.aspect * MainCamera.orthographicSize, MainCamera.orthographicSize, 1) * FollowThreshold * 2.0f);
        }
    }

    Bounds GetWorldBounds() {
        float ySize = MainCamera.orthographicSize;
        float hSize = ySize * Screen.width / Screen.height;
        return new Bounds(MainCamera.transform.position, new Vector3(hSize * FollowThreshold * 2.0f, ySize * FollowThreshold * 2.0f, 20));
    }

}
