using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float musicVolume = 1f;

    private float playDelay = Config.BIG_DELAY;

    [Header("Single Tracks")]
    [Space(2)]

    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip studioMusic;
    [SerializeField] private AudioClip museumMusic;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdateVolume(Settings.Instance.musicVolume);
    }

    public void UpdateVolume(float value)
    {
        musicVolume = value;

        audioSource.volume = musicVolume;
        Settings.Instance.musicVolume = musicVolume;

        Settings.Save();
    }

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
        PlayMusic(scene.name);
    }

    public void PlayMusic(AudioClip audioClip, float delay)
    {
        StopMusic();

        audioSource.loop = true;
        audioSource.clip = audioClip;

        StartCoroutine(WaitAndPlay(delay));
    }

    public void PlayMusic(string sceneName)
    {
        switch (sceneName)
        {
            case Config.MAIN_MENU_SCENE_NAME:

                audioSource.loop = true;
                audioSource.clip = mainMenuMusic;
                StartCoroutine(WaitAndPlay(playDelay));

                break;

            case Config.STUDIO_SCENE_NAME:

                audioSource.loop = true;
                audioSource.clip = studioMusic;
                StartCoroutine(WaitAndPlay(playDelay));

                break;

            case Config.MUSEUM_SCENE_NAME:

                audioSource.loop = true;
                audioSource.clip = museumMusic;
                StartCoroutine(WaitAndPlay(playDelay));

                break;
        }
    }

    public void StopMusic()
    {
        StopAllCoroutines();
        audioSource.Stop();
    }

    private IEnumerator WaitAndPlay(float duration)
    {
        yield return new WaitForSeconds(duration);

        audioSource.Play();
    }

    public string GetCurrentAudioClipName()
    {
        if (audioSource.isPlaying)
            return audioSource.clip.name;

        return "None";
    }

    public void SetLooping(bool value)
    {
        audioSource.loop = value;
    }

}
