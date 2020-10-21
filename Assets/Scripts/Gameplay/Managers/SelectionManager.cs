using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectionManager : MonoBehaviour{
    public static SelectionManager Instance;
    void Awake()
    {
        Instance = this;
    }
    public Vector2Int HoveredTile {
        get {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return new Vector2Int(Mathf.FloorToInt(mousePos.x), Mathf.FloorToInt(mousePos.y));
        }
    }
    public Item ActiveItem;

}
