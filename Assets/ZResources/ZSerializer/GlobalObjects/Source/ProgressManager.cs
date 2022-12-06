using System;
using System.Collections.Generic;
using ZSerializer;

[Serializable, SerializeGlobalData(GlobalDataType.Globally)]
public partial class ProgressManager
{
    public int money;

    public int backpackCapacity;

    public int rope;
    public bool saw;
    public bool protein;

    public List<string> shopItems;
    public List<string> ownedItems;

    public List<string> stolen;

    public void Reset()
    {
        money = 0;

        backpackCapacity = 0;

        rope = 0;

        saw = false;
        protein = false;

        shopItems = new List<string> { "Rope", "Protein", "Saw", "Backpack - Small" };
        ownedItems.Clear();

        stolen.Clear();

        Save();
    }

    public void AddMoney(int newMoney)
    {
        money += newMoney;
        Save();
    }

    public void SetBackpackCapacity(int value)
    {
        backpackCapacity = value;
        Save();

        GameManager.instance.UpdateCarryingCapacityText();
    }

    public void AddRope()
    {
        rope++;
        Save();
    }

    public void SetProtein(bool value)
    {
        protein = value;
        Save();
    }

    public void SetSaw(bool value)
    {
        saw = value;
        Save();
    }

    public void AddShopItem(Item item)
    {
        shopItems.Add(item.name);
        Save();
    }

    public void RemoveShopItem(Item item)
    {
        shopItems.Remove(item.name);
        Save();
    }

    public void AddOwnedItem(Item item)
    {
        ownedItems.Add(item.name);
        Save();
    }

    public void AddStolen(Stealable stealable)
    {
        stolen.Add(stealable.name);
        Save();
    }
}
