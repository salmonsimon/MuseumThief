using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Masterpiece : Collectable
{
    private Sprite none;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = none;
        }
    }


}
