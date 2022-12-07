using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene03 : Cutscene
{
    [Space(5)]

    [Header("Items")]

    [SerializeField] private GameObject duckGem;
    private Transform duckGemOriginalParent;

    [Space(5)]

    [Header("Fireworks")]

    [SerializeField] private GameObject firework1;
    [SerializeField] private GameObject firework2;
    [SerializeField] private GameObject firework3;
    [SerializeField] private GameObject firework4;
    [SerializeField] private GameObject firework5;

    private void Start()
    {
        duckGemOriginalParent = duckGem.transform.parent;
    }

    public void PlayCutscene()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        Player player = GameManager.instance.GetPlayer();

        DeactivatePlayer();
        GameManager.instance.ShowInventoriesUI(false);

        duckGem.transform.parent = GameManager.instance.GetPlayer().transform;
        duckGem.transform.position = GameManager.instance.GetMasterpieceHoldingPosition().transform.position;

        #region Game Cleared Cutscene - Intro

        playableDirector.playableAsset = timelines[0];

        #region Binding

        Bind(playableDirector, "Player Animations", player.gameObject);
        Bind(playableDirector, "Player Movement", player.gameObject);

        #endregion

        playableDirector.Play();

        yield return new WaitForSeconds(2f);

        #endregion

        GameManager.instance.GetSoundManager().PlaySound(Config.THROW_SFX);

        duckGem.transform.parent = duckGemOriginalParent;
        duckGem.transform.position = new Vector3(duckGem.transform.position.x, duckGem.transform.position.y + .16f, duckGem.transform.position.z);

        #region Game Cleared Cutscene - Celebration

        playableDirector.playableAsset = timelines[1];

        #region Bindings

        Bind(playableDirector, "Player Animations", player.gameObject);
        Bind(playableDirector, "Player Movement", player.gameObject);

        Bind(playableDirector, "Firework - 1 - Activation Track", firework1.gameObject);
        Bind(playableDirector, "Firework - 1 - Animation Track", firework1.gameObject);
        Bind(playableDirector, "Firework - 1 - Movement Track", firework1.gameObject);
        Bind(playableDirector, "Firework - 1 - Audio Track", GameManager.instance.GetSoundManager().gameObject);

        Bind(playableDirector, "Firework - 2 - Activation Track", firework2.gameObject);
        Bind(playableDirector, "Firework - 2 - Animation Track", firework2.gameObject);
        Bind(playableDirector, "Firework - 2 - Movement Track", firework2.gameObject);
        Bind(playableDirector, "Firework - 2 - Audio Track", GameManager.instance.GetSoundManager().gameObject);

        Bind(playableDirector, "Firework - 3 - Activation Track", firework3.gameObject);
        Bind(playableDirector, "Firework - 3 - Animation Track", firework3.gameObject);
        Bind(playableDirector, "Firework - 3 - Movement Track", firework3.gameObject);
        Bind(playableDirector, "Firework - 3 - Audio Track", GameManager.instance.GetSoundManager().gameObject);

        Bind(playableDirector, "Firework - 4 - Activation Track", firework4.gameObject);
        Bind(playableDirector, "Firework - 4 - Animation Track", firework4.gameObject);
        Bind(playableDirector, "Firework - 4 - Movement Track", firework4.gameObject);
        Bind(playableDirector, "Firework - 4 - Audio Track", GameManager.instance.GetSoundManager().gameObject);

        Bind(playableDirector, "Firework - 5 - Activation Track", firework5.gameObject);
        Bind(playableDirector, "Firework - 5 - Animation Track", firework5.gameObject);
        Bind(playableDirector, "Firework - 5 - Movement Track", firework5.gameObject);
        Bind(playableDirector, "Firework - 5 - Audio Track", GameManager.instance.GetSoundManager().gameObject);

        #endregion

        playableDirector.Play();

        yield return new WaitForSeconds(4f);

        #endregion

        ActivatePlayer();
    }
}
