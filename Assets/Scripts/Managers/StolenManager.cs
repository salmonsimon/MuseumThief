using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StolenManager : MonoBehaviour
{
    public List<Stealable> carrying = new List<Stealable>();
    public List<Stealable> stolen = new List<Stealable>();

    public Transform stealableContentCarrying;
    public Transform stealableContentStolen;
    public GameObject stolenObject;

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

        GameManager.instance.AddMoney(stealable.price);
        stealable.SetAsSold();
    }

    public void CarryingToStolen()
    {
        foreach (Stealable stealable in carrying)
            AddToStolen(stealable);

        ResetCarrying();
    }

    private void ListItems(List<Stealable> stealableList, Transform stealableContent)
    {
        foreach (Transform stealable in stealableContent)
        {
            Destroy(stealable.gameObject);
        }

        foreach (var stealable in stealableList)
        {
            GameObject obj = Instantiate(stolenObject, stealableContent);

            var name = obj.transform.Find("Item Name").GetComponent<Text>();
            var icon = obj.transform.Find("Item Icon").GetComponent<Image>();

            name.text = stealable.stealableName;
            icon.sprite = stealable.icon;
        }
    }

    public void ListItemsCarrying()
    {
        ListItems(carrying, stealableContentCarrying);
    }

    public void ListItemsStolen()
    {
        ListItems(stolen, stealableContentStolen);
    }
}
