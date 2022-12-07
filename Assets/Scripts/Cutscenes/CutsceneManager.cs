using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private bool playedIntroCutscene = false;
    public bool PlayedIntroCutscene { get { return playedIntroCutscene; } set { playedIntroCutscene = value; } }

    [SerializeField] private bool playedMuseumCutscene = false;
    public bool PlayedMuseumCutscene { get { return playedMuseumCutscene; } set { playedMuseumCutscene = value; } }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!GameManager.instance.IsOnMainMenu())
        {
            switch (scene.name)
            {
                case Config.STUDIO_SCENE_NAME:

                    if (!playedIntroCutscene)
                    {
                        GameObject introCutscene = GameObject.FindGameObjectWithTag(Config.INTRO_CUTSCENE_TAG);

                        if (introCutscene)
                        {
                            introCutscene.GetComponent<Cutscene01>().PlayCutscene();
                        }
                    }

                    break;

                case Config.MUSEUM_SCENE_NAME:

                    if (!playedMuseumCutscene)
                    {
                        GameObject museumCutscene = GameObject.FindGameObjectWithTag(Config.MUSEUM_CUTSCENE_TAG);

                        if (museumCutscene) 
                        {
                            museumCutscene.GetComponent<Cutscene02>().PlayCutscene();
                        }
                    }

                    break;
            }
        }
    }
}
