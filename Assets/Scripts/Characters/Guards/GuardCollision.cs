using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCollision : Collidable
{
    private bool caught = false;

    protected override void Start()
    {
        base.Start();

        caught = false;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !caught && !GameManager.instance.GetPlayer().IsTeleporting())
        {
            caught = true;

            GameManager.instance.GetPlayer().SetIsTeleporting(true);

            GameManager.instance.GetSoundManager().PlaySound(Config.CAUGHT_SFX);

            GameManager.instance.GetStolenManager().ResetCarrying();

            Masterpiece heldMasterpiece = GameManager.instance.GetHeldMasterpiece();

            if (heldMasterpiece)
                heldMasterpiece.Throw();

            GameManager.instance.GetLevelLoader().LoadLevel(Config.STUDIO_SCENE_NAME, Config.CAUGHT_TRANSITION);
        }
    }
}
