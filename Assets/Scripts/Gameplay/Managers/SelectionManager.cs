using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager : MonoBehaviour {
    public static SelectionManager Instance;
    public Vector2Int HoveredTile {
        get {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2Int(Mathf.FloorToInt(mousePos.x), Mathf.FloorToInt(mousePos.y));
        }
    }
    public Item ActiveItem;
    public int SelecetedRotation { get; private set; } = 0;

    void Awake() {
        Instance = this;
    }

    void Update() {
        if(Input.GetButtonDown("Rotate")) {
            // SelecetedRotation--;
            SelecetedRotation = (SelecetedRotation + 3) % 4;
        }
    }


}
