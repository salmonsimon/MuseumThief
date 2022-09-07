using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string itemName;

    [TextArea(3, 10)]
    public string itemDescription;
    
    public Sprite icon;

    public int price;
    public bool infiniteAmount = false;
    public ItemType itemType;

    public enum ItemType
    {
        Backpack,
        Rope,
        Saw,
        Protein
    }
}
