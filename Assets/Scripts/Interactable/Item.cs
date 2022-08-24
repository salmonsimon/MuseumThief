using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public int price;
    public Sprite icon;

    public bool sold = false;
    public bool canBePurchased = true;

    public void SetAsSold()
    {
        sold = true;
    }
}
