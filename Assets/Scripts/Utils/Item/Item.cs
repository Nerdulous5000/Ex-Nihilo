using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Gameplay/Item")]
public class Item : ScriptableObject
{
    public Sprite Sprite;
    public bool IsEntity = false;

}
