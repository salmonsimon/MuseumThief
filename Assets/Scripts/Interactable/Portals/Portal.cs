using System.Collections;
using UnityEngine;

public class Portal : Collidable
{
    [SerializeField] private string[] sceneNames;
    [SerializeField] private bool isRandomPortal = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !GameManager.instance.GetPlayer().IsTeleporting())
        {
            string sceneName;

            if (isRandomPortal)
                sceneName = sceneNames[Random.Range(0, sceneNames.Length)];
            else
                sceneName = sceneNames[0];

            UsePortal(sceneName);
        }
    }

    public async void UsePortal(string sceneName = "Studio")
    {
        GameManager.instance.GetPlayer().SetIsTeleporting(true);

        if (GameManager.instance.GetOnEmergency())
        {
            GameManager.instance.SetOnEmergency(false);
            GameManager.instance.GetSoundManager().StopSoundEffects();
        }

        GameManager.instance.GetSoundManager().PlaySound(Config.PORTAL_SFX);

        GameManager.instance.GetStolenManager().CarryingToStolen();
        GameManager.instance.GetPlayer().ResetToNormalSpeed();

        var masterpieces = FindObjectsOfType<Masterpiece>();

        foreach (Masterpiece masterpiece in masterpieces)
            masterpiece.BackToOriginalPosition();

        await ZSerializer.ZSerialize.SaveScene();
        GameManager.instance.SetGameHasBeenSaved(true);

        GameManager.instance.GetLevelLoader().LoadLevel(sceneName, Config.CROSSFADE_TRANSITION);
    }
}
