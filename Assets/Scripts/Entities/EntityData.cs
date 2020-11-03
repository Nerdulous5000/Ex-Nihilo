
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Gameplay/EntityData")]
public class EntityData : ScriptableObject
{
    public Sprite Sprite;
    public int Width;
    public int Height;
    public ItemInterface Interface;
    
}
