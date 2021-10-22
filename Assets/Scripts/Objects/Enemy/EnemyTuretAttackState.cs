using UnityEngine;
public class EnemyTuretAttackState : EnemyStateFields, IState
{
    public void Enter(params object[] args)
    {
        AddParmsToVaribles(args);
        facingRight = (bool)args[10];
        Debug.Log("Facing Right Turret = " + facingRight.ToString());
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
        RaycastMethod2(facingRight);
        enemy.Shoot2(facingRight);
        if (hitInfo.collider == null)
        {
            stateMachine.Change("patrol", enemy, stateMachine, null, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null, step);
        }
    }
}

