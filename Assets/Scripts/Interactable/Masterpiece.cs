using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masterpiece : Collectable
{
    [SerializeField] private Stealable stealable;
    private Sprite empty;

    protected override void OnCollect()
    {
        if (!collected)
        {
            Debug.Log("First collection");

            collected = true;
            GameManager.instance.GetStolenManager().AddToCarrying(stealable);

            //Destroy(gameObject);
            GetComponent<SpriteRenderer>().sprite = empty;
        }
    }


}
