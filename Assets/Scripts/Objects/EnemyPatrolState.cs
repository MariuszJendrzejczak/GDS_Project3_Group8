﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateFields, IState
{
    private int patrolCounter = 0;

    public void Enter(params object[] args)
    {
        AddParmsToVaribles(args);
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
        FacingCheck();
        Patrol();
        RaycastMethod();
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.tag == "Player")
            {
                stateMachine.Change("attack", enemy, stateMachine, patrolPoints, playerLayerMask, raycastDistance, hitInfo);
                Debug.Log(hitInfo.collider.name);
            }
        }
    }

    private void Patrol()
    {
        if (patrolCounter < patrolPoints.Count)
        {
            if (enemy.transform.position != patrolPoints[patrolCounter].position)
            {
                target = patrolPoints[patrolCounter].position;
            }
            else
            {
                patrolCounter++;
            }
        }
        else
        {
            patrolCounter = 0;
        }
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, target, step);
    }
}

