using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StealedManager : MonoBehaviour
{
    public static StealedManager instance;
    public List<Stealable> stealed = new List<Stealable>();

    public Transform stealableContent;
    public GameObject stealedObject;

    private void Awake()
    {
        instance = this;
    }

    public void Add(Stealable stealable)
    {
        stealed.Add(stealable);
    }

    public void ListItems()
    {
        foreach (Transform stealed in stealableContent)
        {
            Destroy(stealed.gameObject);
        }

        foreach (var stealed in stealed)
        {
            GameObject obj = Instantiate(stealedObject, stealableContent);

            var name = obj.transform.Find("Item Name").GetComponent<Text>();
            var icon = obj.transform.Find("Item Icon").GetComponent<Image>();

            name.text = stealed.stealableName;
            icon.sprite = stealed.icon;
        }
    }
}
