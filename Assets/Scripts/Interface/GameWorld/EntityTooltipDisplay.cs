using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityTooltipDisplay : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject Container;
    public Image TooltipImage;
    public TextMeshProUGUI TooltipText;
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        EntityBehaviour selectedEntity = EntityManager.Instance.At(SelectionManager.HoveredTile);
        if (selectedEntity != null) {
            TooltipImage.sprite = selectedEntity.Sprite;
            TooltipText.text = selectedEntity.TooltipString;
            Container.SetActive(true);
        } else {
            Container.SetActive(false);
        }
    }
}
