using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    //[SerializeField] private Sprite openedPortal;
    [SerializeField] string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            //GetComponent<SpriteRenderer>().sprite = openedPortal;

            //GameManager.instance.SaveState();

            string randomScene = sceneNames[Random.Range(0, sceneNames.Length)];

            UnityEngine.SceneManagement.SceneManager.LoadScene(randomScene);
        }
    }
}
