using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Enemy
{
    [SerializeField] private List<Transform> patrolPoints;
    private enum StartingState { patrol, attack }
    [SerializeField] StartingState startingState;
    protected float localScaleXbase, localScaleXrevers;
    private float startPosX, startPosY;

    protected override void Awake()
    {
        base.Awake();
        stateMachine.Add("patrol", new EnemyPatrolState());
        stateMachine.Add("attack", new EnemyAttackState());

        EventBroker.GiveAllEnemyesOnSceneBulletPoolReference += GetBulletsPool;
        EventBroker.RespawnToCheckPoint += AfterPlayerRespawn;
    }
    protected override void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        localScaleXbase = transform.localScale.x;
        localScaleXrevers = transform.localScale.x * -1f;
        base.Start();
        ChangeState(startingState.ToString());

        EventBroker.PlayerDeath += OnPlayerDeath;
    }
    private void OnDestroy()
    {
        EventBroker.RespawnToCheckPoint -= AfterPlayerRespawn;
    }

    public void ChangeState(string key)
    {
        stateMachine.Change(key, this, stateMachine, patrolPoints, playerLeyerMask, raycastDistance, null, rayCastOffsetX, rayCastOffsetY, null, patrolSpeed);
    }

    public void AfterPlayerRespawn()
    {
        transform.position = new Vector2(startPosX, startPosY);
        ChangeState("patrol");
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
            transform.localScale = new Vector2(0.5f, 0.5f);
            ChangeState("patrol");
        }
    }
    public void FlipMethod2(bool facingRight)
    {
        if (facingRight)
        {
            transform.localScale = new Vector2(localScaleXrevers, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(localScaleXbase, transform.localScale.y);
        }
    }
}
