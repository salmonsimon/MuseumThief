using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Guard : Mover
{

    [SerializeField] private float triggerLenght = 1;
    [SerializeField] private float chaseLenght = 3;
    [SerializeField] private float nextWaypointDistance = 0.08f;

    private bool chasing;
    private bool retreating;
    private bool onChasingPath;
    private Transform playerTransform;
    private Vector3 startingPosition;

    private Seeker seeker;
    private Path path;
    private int currentWaypoint;
    private bool reachedEndOfPath = false;

    protected override void Start()
    {
        base.Start();

        playerTransform = GameManager.instance.GetPlayer().transform;
        startingPosition = transform.position;

        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, .25f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
        if (chasing)
            {
                onChasingPath = true;
                seeker.StartPath(transform.position, playerTransform.position, OnPathComplete);
            }

        else if (retreating)
            {
                onChasingPath = false;
                seeker.StartPath(transform.position, startingPosition, OnPathComplete);
            }
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {

            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
                retreating = false;
                animator.SetBool("Chasing", chasing);
            }

            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;

                if (onChasingPath)
                    retreating = true;
                else
                {
                    retreating = false;
                    UpdateMotor(Vector3.zero, false);
                }

                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            if (chasing)
            {
                Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
                UpdateMotor(direction, false);

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                retreating = true;

                if (retreating && chasing)
                {
                    chasing = false;
                    animator.SetBool("Chasing", chasing);
                    return;
                }

                Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
                UpdateMotor(direction, false);

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }

                chasing = false;
                animator.SetBool("Chasing", chasing);
            }
            
        }
        else
        {
            if (retreating && chasing)
            {
                chasing = false;
                animator.SetBool("Chasing", chasing);
                return;
            }

            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;

                if (onChasingPath)
                    retreating = true;
                else
                {
                    retreating = false;
                    UpdateMotor(Vector3.zero, false);
                }
                
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
            UpdateMotor(direction, false);

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            chasing = false;
            animator.SetBool("Chasing", chasing);
        }
    }

}
