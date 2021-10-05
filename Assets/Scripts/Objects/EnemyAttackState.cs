using UnityEngine;
using System.Collections;
public class EnemyAttackState : EnemyStateFields, IState
{

    public void Enter(params object[] args)
    {
        AddParmsToVaribles(args);
        enemy.animator.SetTrigger("idle");
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
        enemy.animator.SetTrigger("idle");
        enemy.Shoot();
        facing = (Faceing)enemy.EnemyFaceing;
        if (hitInfo.collider == null || hitInfo.collider.tag == "Hide")
        {
            stateMachine.Change("patrol", enemy, stateMachine, patrolPoints, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null, step);
        }
        RaycastMethod();
    }


}

