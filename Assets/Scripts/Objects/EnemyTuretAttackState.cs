public class EnemyTuretAttackState : EnemyStateFields, IState
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
        RaycastMethod();
        enemy.Shoot();
        if (hitInfo.collider == null)
        {
            stateMachine.Change("patrol", enemy, stateMachine, null, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null);
        }
    }
}

