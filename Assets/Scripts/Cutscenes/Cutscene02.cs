using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene02 : Cutscene
{
    [Space(5)]

    [Header("Items")]

    [SerializeField] private GameObject virtualCamera;

    public void PlayCutscene()
    {
        virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 2;
        virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.OrthographicSize = 3.1f;

        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(Config.SMALL_DELAY);

        Player player = GameManager.instance.GetPlayer();

        DeactivatePlayer();
        GameManager.instance.ShowInventoriesUI(false);

        #region Museum Intro Cutscene

        playableDirector.playableAsset = timelines[0];

        Bind(playableDirector, "Camera Movement", virtualCamera.gameObject);

        playableDirector.Play();

        while (playableDirector.state == PlayState.Playing)
        {
            yield return null;
        }

        #endregion

        ActivatePlayer();

        GameManager.instance.GetCutsceneManager().PlayedMuseumCutscene = true;

        virtualCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().Priority = 0;

        GameManager.instance.ShowInventoriesUI(true);

        gameObject.SetActive(false);
    }
}
