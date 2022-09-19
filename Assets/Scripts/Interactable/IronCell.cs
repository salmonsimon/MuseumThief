using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronCell : Collidable
{

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameManager.instance.GetStolenManager().saw)
                {
                    GameManager.instance.GetSoundManager().PlaySound(Config.HOVER_SFX);
                    Destroy(gameObject);

                    GameManager.instance.GetPathfinderGraphUpdater().UpdatePathfinderGraphs();
                }
                else
                {
                    GameManager.instance.GetSoundManager().PlaySound(Config.DENIED_SFX);
                    GameManager.instance.ShowText("Can't cut it without a saw", 24, Color.white, new Vector3(GameManager.instance.GetPlayer().transform.position.x, GameManager.instance.GetPlayer().transform.position.y + 0.16f, 0), Vector3.up * 40, 1f);
                }
            }

        }
    }
}
