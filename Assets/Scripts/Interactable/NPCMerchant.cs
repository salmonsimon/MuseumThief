using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMerchant : Collidable
{

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<AudioSource>().Play();
                GameManager.instance.ShowItemShop();
                GameManager.instance.ListShopItems();
            }

        }
    }
}
