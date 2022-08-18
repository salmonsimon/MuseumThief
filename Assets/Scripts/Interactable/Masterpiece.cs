using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masterpiece : Collectable
{
    [SerializeField] private Stealable stealable;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            StolenManager.instance.AddToCarrying(stealable);
            Destroy(gameObject);
        }
    }


}
