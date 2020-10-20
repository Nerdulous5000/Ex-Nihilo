using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarController : MonoBehaviour {

    public int MaxCount = 0;

    [SerializeField]
    ItemContainer refContainer = null;
    List<ItemContainer> items;
    int activeIndex = 0;
    [SerializeField]
    Image highlightImage = null;

    void Start() {
        items = new List<ItemContainer>();
        for (int n = 0; n < MaxCount; n++) {
            ItemContainer itemContainer = Instantiate(refContainer);
            itemContainer.transform.SetParent(transform, false);
            items.Add(itemContainer);
        }
    }

    // Update is called once per frame
    void Update() {

        setActiveContainer(0);
    }
    public void SetSlot(int index, Sprite sprite) {
        if (index >= MaxCount) {
            return;
        }
        items[index].SetDisplayedItem(sprite);
    }
    void setActiveContainer(int index) {
        if (index >= MaxCount) {
            return;
        }
        activeIndex = index;
        highlightImage.transform.position = items[index].transform.position;
        // TODO: Update selection manager
    }

}
