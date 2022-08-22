using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class StolenManager : MonoBehaviour
{
    public List<Stealable> carrying = new List<Stealable>();
    public List<Stealable> stolen = new List<Stealable>();
    public int money;

    public async void SaveStolenManager()
    {
        var json = JsonUtility.ToJson(this);
        await ZSerializer.ZSerialize.WriteToFileGlobal(GlobalDataType.Globally, $"stolenManager.zsave", json);
    }

    public async void LoadStolenManager()
    {
        string path = Application.persistentDataPath + "/GlobalData/stolenManager.zsave";

        JsonUtility.FromJsonOverwrite(await ZSerializer.ZSerialize.ReadFromFileGlobal(GlobalDataType.Globally, $"stolenManager.zsave"),
                    this);
    }

    public void AddToCarrying(Stealable stealable)
        {
            carrying.Add(stealable);
    }

    public void ResetCarrying()
    {
        carrying.Clear();
    }

    private void AddToStolen(Stealable stealable)
    {
        stolen.Add(stealable);

        AddMoney(stealable.price);
        stealable.SetAsSold();
    }

    public void CarryingToStolen()
    {
        foreach (Stealable stealable in carrying)
            AddToStolen(stealable);

        ResetCarrying();

        SaveStolenManager();
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
