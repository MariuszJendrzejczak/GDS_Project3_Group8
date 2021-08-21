﻿using UnityEngine;
using System.Collections;
public class EnemyAttackState : EnemyStateFields, IState
{

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
        enemy.Shoot();
        facing = (Faceing)enemy.EnemyFaceing;
        if (hitInfo.collider == null)
        {
            stateMachine.Change("patrol", enemy, stateMachine, patrolPoints, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null);
        }
        RaycastMethod();
    }


}

