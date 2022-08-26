using System.Collections.Generic;
using UnityEngine;
using ZSerializer;
using System.IO;

public class StolenManager : MonoBehaviour
{
    public List<Stealable> carrying = new List<Stealable>();
    public List<Stealable> stolen = new List<Stealable>();
    public List<Item> shopItems = new List<Item>();
    public List<Item> ownedItems = new List<Item>();

    public int money;
    public int backpackCapacity;
    public int usedBackpackCapacity;
    public int rope;
    public bool saw;
    public bool spinach;

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

    public void AddToCarrying(Stealable stealable)
    {
        carrying.Add(stealable);
        usedBackpackCapacity += stealable.weight;
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

    public void ShopToOwned(Item item)
    {
        if (!item.infiniteAmount)
            shopItems.Remove(item);

        ownedItems.Add(item);
    }

    public int GetMoney()
    {
        return money;
    }
    public void AddMoney(int newMoney)
    {
        money += newMoney;
    }
}
