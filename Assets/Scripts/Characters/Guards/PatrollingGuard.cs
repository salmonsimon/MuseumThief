using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingGuard : Guard
{
    [SerializeField] Transform[] patrollingPath;
    [SerializeField] private int currentPatrollingPoint = 0;
    [SerializeField] private Transform currentPatrollingGoal;
    [SerializeField] private float roundingDistance = 0.16f;
    [SerializeField] private float waitInPatrollingPoint = 2f;

    private bool waiting = false;
    private bool startedChasing = false;

    protected override void UpdatePath()
    {
        if (seeker.IsDone())
        {
            if (chasing)
            {
                onChasingPath = true;
                seeker.StartPath(transform.position, playerTransform.position, OnPathComplete);
            }
            else if (!waiting)
            {
                onChasingPath = false;
                seeker.StartPath(transform.position, currentPatrollingGoal.position, OnPathComplete);
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (Vector3.Distance(currentPatrollingGoal.position, transform.position) < roundingDistance && !waiting)
            StartCoroutine(ChangePatrollingGoalDelayed());

        if (!startedChasing && !chasing)
        startingPosition = transform.position;

        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {

            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
                Chasing();

                if (!startedChasing)
                {
                    startedChasing = true;
                    startingPosition = transform.position;
                }

            }

            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;

                if (!onChasingPath)
                    UpdateMotor(Vector3.zero, false);

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

                    if (!onChasingPath)
                        UpdateMotor(Vector3.zero, false);

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
                Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
                UpdateMotor(direction, false);

                chasing = false;
                FinishedChasing();

                startedChasing = false;
            }

        }
        else
        {
            if (chasing)
            {
                chasing = false;
                FinishedChasing();

                startedChasing = false;

                return;
            }

            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                
                UpdateMotor(Vector3.zero, false);

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

                    UpdateMotor(Vector3.zero, false);

                    return;
                }
            }

            Vector2 direction = (Vector2)(path.vectorPath[currentWaypoint] - transform.position).normalized;
            UpdateMotor(direction, false);
        }
    }

    IEnumerator ChangePatrollingGoalDelayed()
    {
        waiting = true;

        yield return new WaitForSeconds(waitInPatrollingPoint);

        waiting = false;

        ChangePatrollingGoal();
    }

    private void ChangePatrollingGoal()
    {
        if (currentPatrollingPoint == patrollingPath.Length - 1)
        {
            currentPatrollingPoint = 0;
            currentPatrollingGoal = patrollingPath[0];
        }
        else
        {
            currentPatrollingPoint++;
            currentPatrollingGoal = patrollingPath[currentPatrollingPoint];
        }
    }
}
