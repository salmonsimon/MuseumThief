using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Stealable", menuName ="Stealable/Create New Stealable")]
public class Stealable : ScriptableObject
{
    public string stealableName;
    public int weight;
    public int price;
    public Sprite icon;

    public bool sold = false;

    public void SetAsSold()
    {
        sold = true;
    }
}
