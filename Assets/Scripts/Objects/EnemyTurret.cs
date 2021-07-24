using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Add("patrol", new EnemyTurretPatrolState());
        stateMachine.Add("attack", new EnemyTuretAttackState());
        stateMachine.Change("patrol", this, stateMachine, null, playerLeyerMask, raycastDistance, null, rayCastOffsetX, rayCastOffsetY, facing);
    }
    protected override void Update()
    {
        base.Update();
    }
}
