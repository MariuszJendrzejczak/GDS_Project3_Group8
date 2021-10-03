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
        ChangeState(startingState.ToString());
    }

    public void ChangeState(string key)
    {
        stateMachine.Change(key, this, stateMachine, patrolPoints, playerLeyerMask, raycastDistance, null, rayCastOffsetX, rayCastOffsetY, null, patrolSpeed);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    /*public override void RespawnMe()
    {
        base.RespawnMe();
        ChangeState(startingState.ToString());
    }*/
    private void OnEnable()
    {
        if(respawned)
        {
            ChangeState(startingState.ToString());
        }
    }
}
