using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] private string[] sceneNames;
    [SerializeField] private bool isRandomPortal = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            string sceneName;

            if (isRandomPortal)
                sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            else
                sceneName = sceneNames[0];

            BackToStudio(sceneName);
        }
    }

    public async void BackToStudio(string sceneName = "Studio")
    {
        GameManager.instance.GetStolenManager().CarryingToStolen();

        GameManager.instance.GetPlayer().ResetToNormalSpeed();

        var masterpieces = FindObjectsOfType<Masterpiece>();

        foreach (Masterpiece masterpiece in masterpieces)
            masterpiece.BackToOriginalPosition();

        await ZSerializer.ZSerialize.SaveScene();
        GameManager.instance.SetGameHasBeenSaved(true);

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
