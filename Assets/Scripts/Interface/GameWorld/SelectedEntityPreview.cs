using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedEntityPreview : MonoBehaviour
{
    public bool Enabled = true;
    SpriteRenderer spriteRenderer;
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        Item activeItem = SelectionManager.Instance.ActiveItem;
        if (Enabled && activeItem != null) {
            if(activeItem.IsEntity) {
                spriteRenderer.sprite = activeItem.Sprite;
                Vector2Int pos = SelectionManager.Instance.HoveredTile;
                transform.position = new Vector3(pos.x, pos.y, 0) + new Vector3(.5f, .5f, 0);
                int rotationAmount = SelectionManager.Instance.SelecetedRotation;
                transform.rotation = Quaternion.Euler(0, 0, 90 * rotationAmount);
            }
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
