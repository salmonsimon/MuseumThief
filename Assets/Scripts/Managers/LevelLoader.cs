using System.Collections;
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

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }
    }

    public IEnumerator FinishTransition()
    {
        GameManager.instance.SetGameIsPaused(false);

        //yield return new WaitForSeconds(endTransitionTime);

        switch (lastTransitionType)
        {
            case Config.CROSSFADE_TRANSITION:
                crossFade.SetTrigger("End");
                yield return new WaitForSeconds(endTransitionTime*2);
                crossFade.gameObject.SetActive(false);
                break;

            case Config.CAUGHT_TRANSITION:
                caughtFade.OpenBlackScreen();
                yield return new WaitForSeconds(endTransitionTime);
                caughtFade.gameObject.SetActive(false);
                break;
        }
    }
}
