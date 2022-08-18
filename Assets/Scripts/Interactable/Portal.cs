using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] private string[] sceneNames;
    [SerializeField] private bool isFinalPortal = false;
    [SerializeField] private bool isRandomPortal = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            string sceneName;

            //GameManager.instance.SaveState();

            if (isRandomPortal)
                sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            else
                sceneName = sceneNames[0];

            if (isFinalPortal)
                StealedManager.instance.CarryingToStealed();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
