using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [SerializeField] private List<Transform> patrolPoints;
    private enum StartingState { patrol, attack }
    [SerializeField] StartingState startingState;
    protected override void Awake()
    {
        base.Awake();
        EventBroker.GiveAllTurretsOnSceneBulletPoolRefenece += GetBulletsPool;
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.Add("patrol", new EnemyTurretPatrolState());
        stateMachine.Add("attack", new EnemyTuretAttackState());
        ChangeState(startingState.ToString());
    }

    public void ChangeState(string key)
    {
        stateMachine.Change(key, this, stateMachine, null, playerLeyerMask, raycastDistance, null, rayCastOffsetX, rayCastOffsetY, facing, null);
    }
    protected override void Update()
    {
        base.Update();
    }
    private void OnEnable()
    {
        if (respawned)
        {
            ChangeState(startingState.ToString());
        }
    }
}
