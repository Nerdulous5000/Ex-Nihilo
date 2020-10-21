using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarController : MonoBehaviour {

    public int MaxCount = 0;

    [SerializeField]
    ItemContainer refContainer = null;
    List<ItemContainer> containers;
    public int ActiveIndex = 0;
    [SerializeField]
    Image highlightImage = null;
    [SerializeField]
    TextMeshProUGUI text = null;


    void Awake() {
        containers = new List<ItemContainer>();
        for (int n = 0; n < MaxCount; n++) {
            ItemContainer itemContainer = Instantiate(refContainer);
            itemContainer.transform.SetParent(transform, false);
            containers.Add(itemContainer);
        }
    }

    void Start() {
        SetActiveContainer(0);
    }

    // Update is called once per frame
    void Update() {
        for (int n = 0; n < MaxCount; n++) {
            if (Input.GetButtonDown("Hotbar" + n)) {
                SetActiveContainer(n);
            }
        }
    }
    public void SetSlot(int index, Item item) {
        if (index >= MaxCount) {
            return;
        }
        containers[index].SetItem(item);
    }
    void SetActiveContainer(int index) {
        if (index >= MaxCount) {
            return;
        }
        Item currentItem = containers[index].Item;
        highlightImage.transform.position = containers[index].transform.position;
        SelectionManager.Instance.ActiveItem = currentItem;
        text.text = currentItem == null ? "" : currentItem.Name;
    }

}
