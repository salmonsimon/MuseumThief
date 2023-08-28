using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Guard : Mover
{

    [SerializeField] protected float triggerLenght = 1;
    [SerializeField] protected float chaseLenght = 3;
    [SerializeField] protected float nextWaypointDistance = 0.08f;

    [SerializeField] private float chasingXSpeed = 1.2f;
    [SerializeField] private float chasingYSpeed = 1.2f;

    [SerializeField] protected bool chasing;
    private bool retreating;
    protected bool onChasingPath;
    protected Transform playerTransform;
    [SerializeField] protected Vector3 startingPosition;

    protected Seeker seeker;
    protected Path path;
    protected int currentWaypoint;
    protected bool reachedEndOfPath = false;

    protected override void Start()
    {
        base.Start();

        playerTransform = GameManager.instance.GetPlayer().transform;
        startingPosition = transform.position;

        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, .25f);
    }

    protected virtual void UpdatePath()
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

    protected void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {

            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
                retreating = false;

                Chasing();
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

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }

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
            }

            if (chasing)
            {
                Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
                UpdateMotor(direction, false);
            }
            else
            {
                retreating = true;

                if (retreating && chasing)
                {
                    chasing = false;
                    FinishedChasing();
                }

                Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
                UpdateMotor(direction, false);

                chasing = false;
                FinishedChasing();
            }
            
        }
        else
        {
            if (retreating && chasing)
            {
                chasing = false;
                FinishedChasing();
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

                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;

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
                }
            }

            Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
            UpdateMotor(direction, false);

            chasing = false;
            FinishedChasing();
        }
    }

    public void Chasing()
    {
        xSpeed = chasingXSpeed;
        ySpeed = chasingYSpeed;

        animator.SetBool("Chasing", true);
    }

    public void FinishedChasing()
    {
        ResetToNormalSpeed();

        animator.SetBool("Chasing", false);
    }
}
