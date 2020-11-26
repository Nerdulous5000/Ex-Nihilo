using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Gameplay/Item")]
public class Item : ScriptableObject {
    public Sprite Sprite;
    public string Name;
    // public bool IsEntity = false;
    public bool IsEntity { get { return EntityName != ""; } }
    public string EntityName = "";
    public int Id;
    public int StackSize;


}
