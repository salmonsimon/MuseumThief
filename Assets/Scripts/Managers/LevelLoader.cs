using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator crossFade;
    [SerializeField] private CircleTransition caughtFade;

    private float startTransitionTime = 0.5f;
    private float endTransitionTime = 0.5f;
    private string lastTransitionType;

    public void LoadLevel(string sceneName, string transitionType)
    {
        StartCoroutine(LoadLevelAndAnimate(sceneName, transitionType));
    }

    private IEnumerator LoadLevelAndAnimate(string sceneName, string transitionType)
    {
        switch (transitionType)
        {
            case Config.CROSSFADE_TRANSITION:
                crossFade.gameObject.SetActive(true);
                crossFade.SetTrigger("Start");
                break;

            case Config.CAUGHT_TRANSITION:
                caughtFade.gameObject.SetActive(true);
                caughtFade.CloseBlackScreen();
                break;
        }

        lastTransitionType = transitionType;

        yield return new WaitForSeconds(startTransitionTime);

        GameManager.instance.SetGameIsPaused(true);
        GameManager.instance.GetMusicManager().StopMusic();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator FinishTransition()
    {
        GameManager.instance.SetGameIsPaused(false);

        switch (lastTransitionType)
        {
            case Config.CROSSFADE_TRANSITION:
                crossFade.SetTrigger("End");
                //yield return new WaitForSeconds(endTransitionTime*2);
                yield return new WaitForSeconds(endTransitionTime);
                crossFade.gameObject.SetActive(false);
                break;

            case Config.CAUGHT_TRANSITION:
                caughtFade.OpenBlackScreen();
                yield return new WaitForSeconds(endTransitionTime);
                caughtFade.gameObject.SetActive(false);
                break;
        }

        List<Stealable> lastCarrying = GameManager.instance.GetStolenManager().GetLastCarrying();

        if (lastCarrying.Count > 0)
        {
            bool broughtDuckGemBack = false;

            foreach (Stealable stealable in lastCarrying)
            {
                if (stealable.name == "Duck Gem")
                {
                    broughtDuckGemBack = true;
                    break;
                }
            }

            if (broughtDuckGemBack)
            {
                Debug.Log("Brought duck gem back home");

                GameObject gameClearedCutscene = GameObject.FindGameObjectWithTag(Config.GAME_CLEARED_CUTSCENE_TAG);

                if (gameClearedCutscene)
                {
                    gameClearedCutscene.GetComponent<Cutscene03>().PlayCutscene();
                }

                yield return new WaitForSeconds(5.3f);

                crossFade.gameObject.SetActive(true);
                crossFade.SetTrigger("Start");

                yield return new WaitForSeconds(startTransitionTime + Config.SMALL_DELAY);

                GameManager.instance.GetPlayer().transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
                GameManager.instance.ShowInventoriesUI(true);

                crossFade.SetTrigger("End");
                yield return new WaitForSeconds(endTransitionTime * 2);
                crossFade.gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(endTransitionTime);

            GameManager.instance.SoldDialogues(lastCarrying);
            GameManager.instance.GetStolenManager().ResetLastCarrying();
        }

        GameManager.instance.GetPlayer().SetIsTeleporting(false);
    }
}
