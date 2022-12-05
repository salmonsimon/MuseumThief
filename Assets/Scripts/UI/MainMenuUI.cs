using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider sfxVolumeSlider;

    public void SetAudioSlidersVolumes()
    {
        musicVolumeSlider.value = Settings.Instance.musicVolume;
        sfxVolumeSlider.value = Settings.Instance.SFXVolume;
    }
}
