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

    #endregion

    #region Item Shop Variables

    public List<Item> shopItems = new List<Item>();
    public List<Item> ownedItems = new List<Item>();

    #endregion

    #region Main Variables

    public int money;

    #endregion

    #region Item variables

    public int backpackCapacity;
    public int usedBackpackCapacity;
    public int rope;
    public bool saw;
    public bool spinach;

    #endregion

    #region Save/Load System

    public async void SaveStolenManager()
    {
        var json = JsonUtility.ToJson(this);
        await ZSerializer.ZSerialize.WriteToFileGlobal(GlobalDataType.Globally, $"stolenManager.zsave", json);
    }

    public async void LoadStolenManager()
    {
        string path = Application.persistentDataPath + "/GlobalData/stolenManager.zsave";

        if (File.Exists(path))
            JsonUtility.FromJsonOverwrite(await ZSerializer.ZSerialize.ReadFromFileGlobal(GlobalDataType.Globally, $"stolenManager.zsave"),
                    this);
    }

    #endregion

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

    private void AddToStolen(Stealable stealable)
    {
        stolen.Add(stealable);

        AddMoney(stealable.price);
        stealable.SetAsSold();
    }

    public void CarryingToStolen()
    {
        Masterpiece heldMasterpiece = GameManager.instance.GetHeldMasterpiece();

        if (heldMasterpiece)
            heldMasterpiece.PutInBag();

        foreach (Stealable stealable in carrying)
            AddToStolen(stealable);

        ResetCarrying();

        SaveStolenManager();
    }

    #endregion

    #region Item Shop

    public void ShopToOwned(Item item)
    {
        if (!item.infiniteAmount)
            shopItems.Remove(item);

        ownedItems.Add(item);
    }

    #endregion

    #region Getters and Setters

    public int GetMoney()
    {
        return money;
    }
    public void AddMoney(int newMoney)
    {
        money += newMoney;
    }

    public int GetUsedCarryingCapacity()
    {
        return usedBackpackCapacity;
    }

    public void SetUsedCapacity(int value)
    {
        usedBackpackCapacity = value;
        GameManager.instance.UpdateCarryingCapacityText();
    }

    public int GetCarryingCapacity()
    {
        return backpackCapacity;
    }

    public void SetCarryingCapacity(int value)
    {
        backpackCapacity = value;
        GameManager.instance.UpdateCarryingCapacityText();
    }

    #endregion
}
