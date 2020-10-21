using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour {
    [SerializeField]
    private Image display = null;
    public Item Item { get; private set; }
    
    public void SetItem(Item item) {
        Item = item;
        display.sprite = item.Sprite;
    }
    public void Clear() {
        Item = null;
        display.sprite = null;
    }

}
