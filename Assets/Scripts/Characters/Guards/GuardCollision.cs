using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCollision : Collidable
{
    [SerializeField] private string MAIN_SCENE = "Testing";

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.instance.GetStolenManager().ResetCarrying();
            UnityEngine.SceneManagement.SceneManager.LoadScene(MAIN_SCENE);
        }
    }
}
