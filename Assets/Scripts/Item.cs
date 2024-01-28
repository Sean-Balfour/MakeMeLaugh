using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "LLL/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
}
