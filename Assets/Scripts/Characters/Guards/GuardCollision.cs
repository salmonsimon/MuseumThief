using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCollision : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.instance.GetSoundManager().PlaySound(Config.CAUGHT_SFX);

            GameManager.instance.GetStolenManager().ResetCarrying();

            Masterpiece heldMasterpiece = GameManager.instance.GetHeldMasterpiece();

            if (heldMasterpiece)
                heldMasterpiece.Throw();

            UnityEngine.SceneManagement.SceneManager.LoadScene(Config.STUDIO_SCENE_NAME);
        }
    }
}
