using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCollision : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.instance.GetStolenManager().ResetCarrying();
            UnityEngine.SceneManagement.SceneManager.LoadScene(Config.MAIN_SCENE_NAME);
        }
    }
}
