using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Enemy
{
    [SerializeField] private List<Transform> patrolPoints;
    private enum StartingState { patrol, attack }
    [SerializeField] StartingState startingState;
    protected override void Awake()
    {
        base.Awake();
        stateMachine.Add("patrol", new EnemyPatrolState());
        stateMachine.Add("attack", new EnemyAttackState());
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Change(startingState.ToString(), this, stateMachine, patrolPoints, playerLeyerMask, raycastDistance, null, rayCastOffsetX, rayCastOffsetY, null);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
