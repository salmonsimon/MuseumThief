using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip grabSFX, throwSFX, storeSFX;
    private AudioClip clickSFX, deniedSFX, hoverSFX, boughtSFX;
    private AudioClip caughtSFX;
    private AudioClip portalSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        grabSFX = Resources.Load<AudioClip>("Audio/Sound FX/Grab");
        throwSFX = Resources.Load<AudioClip>("Audio/Sound FX/Throw");
        storeSFX = Resources.Load<AudioClip>("Audio/Sound FX/Store");

        clickSFX = Resources.Load<AudioClip>("Audio/Sound FX/Click");
        deniedSFX = Resources.Load<AudioClip>("Audio/Sound FX/Denied");
        hoverSFX = Resources.Load<AudioClip>("Audio/Sound FX/Hover");
        boughtSFX = Resources.Load<AudioClip>("Audio/Sound FX/Bought");

        caughtSFX = Resources.Load<AudioClip>("Audio/Sound FX/Caught");

        portalSFX = Resources.Load<AudioClip>("Audio/Sound FX/Portal");
    }

    public void PlaySound(string str)
    {
        switch (str)
        {
        case Config.GRAB_SFX:
            audioSource.PlayOneShot(grabSFX);
            break;

        case Config.THROW_SFX:
            audioSource.PlayOneShot(throwSFX);
            break;

        case Config.STORE_SFX:
            audioSource.PlayOneShot(storeSFX);
            break;

        case Config.CLICK_SFX:
            audioSource.PlayOneShot(clickSFX);
            break;

        case Config.DENIED_SFX:
            audioSource.PlayOneShot(deniedSFX);
            break;

        case Config.HOVER_SFX:
            audioSource.PlayOneShot(hoverSFX);
            break;

        case Config.BOUGHT_SFX:
            audioSource.PlayOneShot(boughtSFX);
            break;

        case Config.CAUGHT_SFX:
            audioSource.PlayOneShot(caughtSFX);
            break;

        case Config.PORTAL_SFX:
            audioSource.PlayOneShot(portalSFX);
            break;
        }
    }
}