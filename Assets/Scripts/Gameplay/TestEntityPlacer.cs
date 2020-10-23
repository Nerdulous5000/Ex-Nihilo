﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////
// TODO: Remove
////////////////////////////////////////////////////
public class TestEntityPlacer : MonoBehaviour {
    // Start is called before the first frame update

    [SerializeField]
    HotbarController hotbar = null;

    [SerializeField]
    List<Item> hotbarItems = new List<Item>();

    void Start() {

        for (int n = 0; n < hotbarItems.Count; n++) {
            hotbar.SetSlot(n, hotbarItems[n]);
        }
    }

    // Update is called once per frame
    void Update() {
        Vector2Int hoverPos = SelectionManager.Instance.HoveredTile;
        if (Input.GetMouseButtonDown(0)) {

            // Interact with Entity if already exists
            if (!EntityManager.Instance.IsNullAt(hoverPos)) {
                EntityManager.Instance.At(hoverPos).OnUse();
            }

            // Place 
            if (SelectionManager.Instance.ActiveItem != null) {
                if (SelectionManager.Instance.ActiveItem.IsEntity) {
                    bool spawned = EntityManager.Instance.Spawn(SelectionManager.Instance.ActiveItem.EntityName, SelectionManager.Instance.HoveredTile);
                    // if (!spawned) {
                    //     Debug.Log("Could not spawn entity at location: " + hoverPos);
                    // }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            bool killed = EntityManager.Instance.Kill(hoverPos);
            // if (!killed) {
            //     Debug.Log("Could not remove entity at location: " + hoverPos);
            // }
        }
    }
}