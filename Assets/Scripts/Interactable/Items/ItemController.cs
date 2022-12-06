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
                int backpackCapacity = ProgressManager.Instance.backpackCapacity;

                ProgressManager.Instance.SetBackpackCapacity(backpackCapacity += 3);

                if (backpackCapacity == 3)
                {
                    Item itemToAdd = Resources.Load<Item>("Items/Backpack - Medium");

                    GameManager.instance.GetStolenManager().shopItems.Add(itemToAdd);
                    ProgressManager.Instance.AddShopItem(itemToAdd);
                }
                else if (backpackCapacity == 6)
                {
                    Item itemToAdd = Resources.Load<Item>("Items/Backpack - Big");

                    GameManager.instance.GetStolenManager().shopItems.Add(itemToAdd);
                    ProgressManager.Instance.AddShopItem(itemToAdd);
                }

                break;

            case Item.ItemType.Rope:
                ProgressManager.Instance.rope += 1;

                break;

            case Item.ItemType.Saw:
                ProgressManager.Instance.saw = true;

                break;

            case Item.ItemType.Protein:
                ProgressManager.Instance.protein = true;

                break;
        }
    }
}