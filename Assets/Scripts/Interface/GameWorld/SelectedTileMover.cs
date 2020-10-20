using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileMover : MonoBehaviour {
    // Start is called before the first frame update
    public bool Enabled = true;
    SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        if (Enabled) {
            transform.position = (Vector3Int)SelectionManager.HoveredTile;
        }
    }
    public void Enable() {
        spriteRenderer.enabled = true;
        Enabled = true;
    }
    public void Disable() {
        spriteRenderer.enabled = false;
        Enabled = false;
    }
}
