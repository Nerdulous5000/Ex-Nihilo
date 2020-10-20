using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer : MonoBehaviour {
    [SerializeField]
    private Image display = null;
    // void Start()
    // {

    // }

    // void Update()
    // {

    // }
    public void SetDisplayedItem(Sprite sprite) {
        display.sprite = sprite;
    }
}
