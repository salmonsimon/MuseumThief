using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPortal : Collidable
{
    [SerializeField] private GameObject teleportPoint;

    protected async override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            GameManager.instance.GetPlayer().transform.position = teleportPoint.transform.position;
        }
    }
}
