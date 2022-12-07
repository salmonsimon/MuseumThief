using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene01 : Cutscene
{
    [Space(5)]

    [Header("Items")]

    [SerializeField] private GameObject duckGem;

    [Space(5)]

    [Header("Emergency Alarm")]

    [SerializeField] private Animator emergencyAlarm;

    [Space(5)]

    [Header("Guards")]

    [SerializeField] private GameObject upperLeftGuard;
    [SerializeField] private GameObject upperRightGuard;
    [SerializeField] private GameObject lowerLeftGuard;
    [SerializeField] private GameObject lowerRightGuard;

    public void PlayCutscene()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(Config.SMALL_DELAY);

        Player player = GameManager.instance.GetPlayer();

        DeactivatePlayer();
        GameManager.instance.ShowInventoriesUI(false);

        #region Game Intro Cutscene - Intro

        playableDirector.playableAsset = timelines[0];

        Bind(playableDirector, "Player Animations", player.gameObject);
        Bind(playableDirector, "Player Movement", player.gameObject);

        playableDirector.Play();

        while (playableDirector.state == PlayState.Playing)
        {
            yield return null;
        }

        #endregion

        GameManager.instance.GetSoundManager().PlaySound(Config.GRAB_SFX);

        duckGem.transform.parent = GameManager.instance.GetPlayer().transform;
        duckGem.transform.position = GameManager.instance.GetMasterpieceHoldingPosition().transform.position;

        emergencyAlarm.SetTrigger("Emergency");

        #region Game Intro Cutscene - Getting Caught

        yield return new WaitForSeconds(Config.BIG_DELAY);

        playableDirector.playableAsset = timelines[1];

        #region Bindings

        Bind(playableDirector, "Player Animations", player.gameObject);
        Bind(playableDirector, "Player Movement", player.gameObject);

        Bind(playableDirector, "Guard - UL - Activation Track", upperLeftGuard);
        Bind(playableDirector, "Guard - UL - Animation Track", upperLeftGuard);
        Bind(playableDirector, "Guard - UL - Movement Track", upperLeftGuard);

        Bind(playableDirector, "Guard - UR - Activation Track", upperRightGuard);
        Bind(playableDirector, "Guard - UR - Animation Track", upperRightGuard);
        Bind(playableDirector, "Guard - UR - Movement Track", upperRightGuard);

        Bind(playableDirector, "Guard - LL - Activation Track", lowerLeftGuard);
        Bind(playableDirector, "Guard - LL - Animation Track", lowerLeftGuard);
        Bind(playableDirector, "Guard - LL - Movement Track", lowerLeftGuard);

        Bind(playableDirector, "Guard - LR - Activation Track", lowerRightGuard);
        Bind(playableDirector, "Guard - LR - Animation Track", lowerRightGuard);
        Bind(playableDirector, "Guard - LR - Movement Track", lowerRightGuard);

        #endregion

        playableDirector.Play();

        yield return new WaitForSeconds(5.5f);

        #endregion

        GameManager.instance.GetSoundManager().PlaySound(Config.CAUGHT_SFX);

        duckGem.transform.parent = null;
        duckGem.transform.position = new Vector3(duckGem.transform.position.x, duckGem.transform.position.y - .64f, duckGem.transform.position.z);

        GameManager.instance.GetLevelLoader().LoadLevel(Config.STUDIO_SCENE_NAME, Config.CAUGHT_TRANSITION);

        ActivatePlayer();

        emergencyAlarm.gameObject.SetActive(false);

        GameManager.instance.GetCutsceneManager().PlayedIntroCutscene = true;

        gameObject.SetActive(false);
    }
}
