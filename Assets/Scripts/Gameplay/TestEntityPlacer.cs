using System.Collections;
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
        // Populate hotbar
        for (int n = 0; n < hotbarItems.Count; n++) {
            hotbar.SetSlot(n, hotbarItems[n]);
        }
    }

    void Update() {
        Vector2Int hoverPos = SelectionManager.Instance.HoveredTile;

        Entities.EntityManager manager = Entities.EntityManager.Instance;
        if(Input.GetMouseButtonDown(0)) {
            // Interact with Entity if already exists
            if (!manager.IsNullAt(hoverPos)) {
                manager.At(hoverPos).OnUse();
            }

        }
        
        if (Input.GetMouseButton(0)) {
            // Place 
            if (SelectionManager.Instance.ActiveItem != null) {
                if (SelectionManager.Instance.ActiveItem.IsEntity) {
                    bool spawned = manager.Spawn(SelectionManager.Instance.ActiveItem.EntityName, SelectionManager.Instance.HoveredTile);
                    // if (!spawned) {
                    //     Debug.Log("Could not spawn entity at location: " + hoverPos);
                    // }
                }
            }
        }

        // Kill entity
        if (Input.GetMouseButton(1)) {
            bool killed = manager.Kill(hoverPos);
            // if (!killed) {
            //     Debug.Log("Could not remove entity at location: " + hoverPos);
            // }
        }
    }
}
