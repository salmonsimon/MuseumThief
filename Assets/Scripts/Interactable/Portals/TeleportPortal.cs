using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : Collidable
{
    [SerializeField] private GameObject teleportPoint;
    [SerializeField] private AudioClip newSong;
    private AudioSource audioSource;

    private bool teleporting = false;

    protected override void Start()
    {
        base.Start();

        audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !teleporting)
        {
            teleporting = true;

            GameManager.instance.GetSoundManager().PlaySound(Config.PORTAL_SFX);
            GameManager.instance.GetPlayer().transform.position = teleportPoint.transform.position;

            if (newSong)
            {
                audioSource.clip = newSong;
                audioSource.Play();
            }

            StartCoroutine(AsyncTeleportRestore());
        }
    }

    private IEnumerator AsyncTeleportRestore()
    {
        yield return new WaitForSeconds(0.5f);

        teleporting = false;
    }
}
