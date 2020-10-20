using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PerfectPixelZoom : MonoBehaviour {


    public PixelPerfectCamera PPCamera;
    public GameObject Target;

    public float ScrollSpeed;
    public float MinZoom;
    public float MaxZoom;

    float currentZoom = 1;
    float initialPpu;
    // Start is called before the first frame update
    void Start() {
        initialPpu = PPCamera.assetsPPU;
    }

    // Update is called once per frame
    void Update() {
        currentZoom += Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        currentZoom = Mathf.Clamp(currentZoom, MinZoom, MaxZoom);

        PPCamera.assetsPPU = Mathf.FloorToInt(initialPpu * currentZoom);
    }
}
