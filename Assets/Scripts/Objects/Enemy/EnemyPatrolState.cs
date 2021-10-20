using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyStateFields, IState
{
    private int patrolCounter = 0;

    public void Enter(params object[] args)
    {
        AddParmsToVaribles(args);
        patrolCounter = 0;
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
        Patrol();
        RaycastMethod2(CheckIfFaceingRight());
        //FacingCheck(); - old method
        //FlipMethod(); - old method
        var pawn = enemy.GetComponent<EnemyPawn>();
        if (pawn != null)
        {
            pawn.FlipMethod2(CheckIfFaceingRight());
        }
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.tag == "Player")
            {
                stateMachine.Change("attack", enemy, stateMachine, patrolPoints, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null, step);
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
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, target, step * Time.deltaTime);
        enemy.animator.SetTrigger("move");
    }
}

