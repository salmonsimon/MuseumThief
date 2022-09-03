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
                GameManager.instance.GetSoundManager().PlaySound(Config.HOVER_SFX);
                GameManager.instance.ShowItemShop();
                GameManager.instance.ListShopItems();
            }

        }
    }
}
