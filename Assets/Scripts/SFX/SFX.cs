using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public void PlayAudioClip(AudioClip audioClip)
    {
        GameManager.instance.GetSoundManager().PlaySound(audioClip);
    }
}
