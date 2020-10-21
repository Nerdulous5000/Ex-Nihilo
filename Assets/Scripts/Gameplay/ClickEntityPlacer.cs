using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////////////////////////////////////////////////
// TODO: Remove
////////////////////////////////////////////////////
public class ClickEntityPlacer : MonoBehaviour {
    // Start is called before the first frame update
    void Start() {

        // Entity miner2 = Instantiate(Resources.Load<Entity>("Entities/AirMiner"));
        // miner2.Position = new Vector2Int(1, 2);
        // if (!miner2.Spawn())
        // {
        //     Debug.Log("Error spawning entity");
        // }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            bool spawned = EntityManager.Instance.Spawn("Storage", SelectionManager.Instance.HoveredTile);
            if (!spawned) {
                Debug.Log("Could not spawn entity at location: " + SelectionManager.Instance.HoveredTile);
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            bool killed = EntityManager.Instance.Kill(SelectionManager.Instance.HoveredTile);
            if (!killed) {
                Debug.Log("Could not remove entity at location: " + SelectionManager.Instance.HoveredTile);
            }
        }
    }
}
