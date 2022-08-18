using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealedManager : MonoBehaviour
{
    public static StealedManager instance;
    public List<Stealable> carrying = new List<Stealable>();
    public List<Stealable> stealed = new List<Stealable>();

    public Transform stealableContentCarrying;
    public Transform stealableContentStealed;
    public GameObject stealedObject;

    private void Awake()
    {
        instance = this;
    }

    public void AddToCarrying(Stealable stealable)
        {
            carrying.Add(stealable);
        }

    public void ResetCarrying()
    {
        carrying = new List<Stealable>();
    }

    private void AddToStealed(Stealable stealable)
    {
        stealed.Add(stealable);

        GameManager.instance.AddMoney(stealable.price);
        stealable.SetAsSold();
    }

    public void CarryingToStealed()
    {
        foreach (Stealable stealable in carrying)
            AddToStealed(stealable);

        ResetCarrying();
    }

    private void ListItems(List<Stealable> stealableList, Transform stealableContent)
    {
        foreach (Transform stealed in stealableContent)
        {
            Destroy(stealed.gameObject);
        }

        foreach (var stealed in stealableList)
        {
            GameObject obj = Instantiate(stealedObject, stealableContent);

            var name = obj.transform.Find("Item Name").GetComponent<Text>();
            var icon = obj.transform.Find("Item Icon").GetComponent<Image>();

            name.text = stealed.stealableName;
            icon.sprite = stealed.icon;
        }
    }

    public void ListItemsCarrying()
    {
        ListItems(carrying, stealableContentCarrying);
    }

    public void ListItemsStealed()
    {
        ListItems(stealed, stealableContentStealed);
    }
}
