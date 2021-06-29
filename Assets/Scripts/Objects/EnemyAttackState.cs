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
        FacingCheck();
        RaycastMethod();
        enemy.Shoot();
        if (hitInfo.collider == null)
        {
            stateMachine.Change("patrol", enemy, stateMachine, patrolPoints, playerLayerMask, raycastDistance, hitInfo);
        }
    }
}

