using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraController : MonoBehaviour {
    public Camera MainCamera;
    public float MoveSpeed;
    [Range(0.0f, 1.0f)]
    public float PanThreshold;
    public float SprintModifier = 1;
    public float ScrollSpeed;
    public float MinZoom;
    public float MaxZoom;

    float currentZoom = 1;
    float speedModifier = 1;
    float initialZoom;
    [SerializeField]
    bool debug;

    void Start() {
        initialZoom = MainCamera.orthographicSize;
    }

    void Update() {
        speedModifier = Input.GetButton("Sprint") ? SprintModifier : 1;

        MainCamera.gameObject.transform.position += GetCameraMoveVector() * MoveSpeed * speedModifier * MainCamera.orthographicSize * Time.deltaTime;

        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        currentZoom = Mathf.Clamp(currentZoom, MinZoom, MaxZoom);

        MainCamera.orthographicSize = initialZoom * currentZoom;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(MainCamera.transform.position, new Vector3(MainCamera.aspect * MainCamera.orthographicSize, MainCamera.orthographicSize, 1) * 2.0f * (1.0f - 2.0f * PanThreshold));
    }

    Vector3 GetCameraMoveVector() {
        Vector3 moveVec = new Vector3();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            moveVec.x = Input.GetAxis("Horizontal");
            moveVec.y = Input.GetAxis("Vertical");
        } else {
            float u = Input.mousePosition.x / Screen.width;
            float v = Input.mousePosition.y / Screen.height;

            bool outOfBounds = u < 0f || u > 1f || v < 0f || v > 1f;
            if (!outOfBounds) {
                // Check right boundary
                if (u > 1.0f - PanThreshold) {
                    moveVec.x = 1;
                }
                // Check left boundary
                if (u < PanThreshold) {
                    moveVec.x = -1;
                }
                // Check up boundary
                if (v > 1.0f - PanThreshold) {
                    moveVec.y = 1;
                }
                // Check down boundary
                if (v < PanThreshold) {
                    moveVec.y = -1;
                }
            }
        }
        return moveVec;
    }
}
