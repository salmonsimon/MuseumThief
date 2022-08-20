using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class Masterpiece : Collectable
{
    [SerializeField] private Stealable stealable;
    [NonZSerialized] private Sprite empty;

    protected override void OnCollect()
    {
        if (!collected)
        {
            Debug.Log("First collection");

            collected = true;
            GameManager.instance.GetStolenManager().AddToCarrying(stealable);

            
            GetComponent<SpriteRenderer>().sprite = empty;
            GetComponent<BoxCollider2D>().enabled = false;

        }
    }


}
