using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] private string[] sceneNames;
    [SerializeField] private bool isFinalPortal = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {

            //GameManager.instance.SaveState();

            string randomScene = sceneNames[Random.Range(0, sceneNames.Length)];


            UnityEngine.SceneManagement.SceneManager.LoadScene(randomScene);
        }
    }
}
