using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : Mover
{

    [SerializeField] private float triggerLenght = 1;
    [SerializeField] private float chaseLenght = 3;

    private bool chasing;
    private bool collidingWithPlayer;
    //private Transform playerTransform;
    [SerializeField]  private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    private Collider2D[] hits = new Collider2D[10];
    public ContactFilter2D filter;

    protected override void Start()
    {
        base.Start();

        //playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                chasing = true;

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized, false);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position, false);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position, false);
            chasing = false;
        }

        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }

            if (hits[i].tag == "Player")
            {
                collidingWithPlayer = true;
            }

            hits[i] = null;
        }
    }

}
