using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Backpack:
                GameManager.instance.GetStolenManager().backpackCapacity += 3;

                if (GameManager.instance.GetStolenManager().backpackCapacity == 3)
                    GameManager.instance.GetStolenManager().shopItems.Add(Resources.Load<Item>("Items/Backpack - Medium"));
                else if (GameManager.instance.GetStolenManager().backpackCapacity == 6)
                    GameManager.instance.GetStolenManager().shopItems.Add(Resources.Load<Item>("Items/Backpack - Big"));

                break;

            case Item.ItemType.Rope:
                GameManager.instance.GetStolenManager().rope += 1;
                break;

            case Item.ItemType.Saw:
                GameManager.instance.GetStolenManager().saw = true;
                break;

            case Item.ItemType.Spinach:
                GameManager.instance.GetStolenManager().spinach = true;
                break;
        }
    }
}