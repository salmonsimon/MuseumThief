using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : Collidable
{
    [SerializeField] private GameObject teleportPoint;
    [SerializeField] private AudioClip newSong;
    private AudioSource audioSource;

    protected override void Start()
    {
        base.Start();

        audioSource = GameObject.FindWithTag("Audio").GetComponent<AudioSource>();
    }

    protected override void OnCollide(Collider2D coll)
    {
        Player player = GameManager.instance.GetPlayer();

        if (coll.CompareTag("Player") && !player.IsTeleporting())
        {
            player.SetIsTeleporting(true);

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
        Player player = GameManager.instance.GetPlayer();

        yield return new WaitForSeconds(0.5f);

        player.SetIsTeleporting(false);
    }
}
