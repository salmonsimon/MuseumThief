using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] private string[] sceneNames;
    [SerializeField] private bool isRandomPortal = false;

    protected async override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            string sceneName;

            if (isRandomPortal)
                sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            else
                sceneName = sceneNames[0];

            GameManager.instance.GetStolenManager().CarryingToStolen();

            //Save game here
            await ZSerializer.ZSerialize.SaveScene();

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
