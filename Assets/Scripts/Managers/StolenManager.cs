using System.Collections.Generic;
using UnityEngine;
using ZSerializer;
using System.IO;
using UnityEngine.UI;

public class StolenManager : MonoBehaviour
{
    #region Carrying/Stolen Variables

    public List<Stealable> carrying = new List<Stealable>();
    public List<Stealable> stolen = new List<Stealable>();
    public List<Stealable> lastCarrying = new List<Stealable>();

    #endregion

    #region Item Shop Variables

    public List<Item> shopItems = new List<Item>();
    public List<Item> ownedItems = new List<Item>();

    #endregion

    #region Variables

    public int usedBackpackCapacity;

    #endregion

    public void LoadStolenManager()
    {
        stolen.Clear();
        shopItems.Clear();
        ownedItems.Clear();

        foreach (string name in ProgressManager.Instance.stolen)
        {
            Stealable stealableToAdd = Resources.Load<Stealable>("Stealables/" + name);

            stolen.Add(stealableToAdd);
        }

        foreach (string name in ProgressManager.Instance.shopItems)
        {
            Item itemToAdd = Resources.Load<Item>("Items/" + name);

            shopItems.Add(itemToAdd);
        }

        foreach (string name in ProgressManager.Instance.ownedItems)
        {
            Item itemToAdd = Resources.Load<Item>("Items/" + name);

            ownedItems.Add(itemToAdd);
        }
    }

    #region Carrying and Stolen

    public void AddToCarrying(Stealable stealable)
    {
        carrying.Add(stealable);
        SetUsedCapacity(usedBackpackCapacity + stealable.weight);
    }

    public void ResetCarrying()
    {
        carrying.Clear();
        usedBackpackCapacity = 0;
    }

    public void ResetLastCarrying()
    {
        lastCarrying.Clear();
    }

    private void AddToStolen(Stealable stealable)
    {
        stolen.Add(stealable);
        ProgressManager.Instance.AddStolen(stealable);

        ProgressManager.Instance.AddMoney(stealable.price);

        stealable.SetAsSold();
    }

    public void CarryingToStolen()
    {
        Masterpiece heldMasterpiece = GameManager.instance.GetHeldMasterpiece();

        if (heldMasterpiece)
            heldMasterpiece.PutInBag();

        foreach (Stealable stealable in carrying)
        {
            AddToStolen(stealable);
            lastCarrying.Add(stealable);
        }

        ResetCarrying();
    }

    #endregion

    #region Item Shop

    public void ShopToOwned(Item item)
    {
        if (!item.infiniteAmount)
        {
            shopItems.Remove(item);
            ProgressManager.Instance.RemoveShopItem(item);
        }

        ownedItems.Add(item);
        ProgressManager.Instance.AddOwnedItem(item);
    }

    #endregion

    #region Getters and Setters

    public int GetUsedCarryingCapacity()
    {
        return usedBackpackCapacity;
    }

    public void SetUsedCapacity(int value)
    {
        usedBackpackCapacity = value;

        GameManager.instance.UpdateCarryingCapacityText();
    }

    public List<Stealable> GetLastCarrying()
    {
        return lastCarrying;
    }

    #endregion
}
