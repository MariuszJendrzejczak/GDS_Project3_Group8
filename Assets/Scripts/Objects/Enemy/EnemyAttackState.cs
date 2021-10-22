using UnityEngine;
using System.Collections;
public class EnemyAttackState : EnemyStateFields, IState
{
    public void Enter(params object[] args)
    {
        Debug.Log("Facing Right Enter = " + facingRight.ToString());
        facingRight = (bool)args[10];
        Debug.Log("Facing Right  Enter2= " + facingRight.ToString());
        AddParmsToVaribles(args);
        enemy.animator.SetTrigger("idle");
        RaycastMethod2(facingRight);
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
    }

    public void Update()
    {
        Debug.Log("Facing Right = " + facingRight.ToString());
        enemy.animator.SetTrigger("idle");
        enemy.Shoot2(facingRight);
        facing = (Faceing)enemy.EnemyFaceing;
        if (hitInfo.collider == null || hitInfo.collider.tag == "Hide")
        {
            stateMachine.Change("patrol", enemy, stateMachine, patrolPoints, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null, step);
        }
        /*if(CheckIfRaycastHitPlayer() == false)
        {
            stateMachine.Change("patrol", enemy, stateMachine, patrolPoints, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null, step);
        }*/
        RaycastMethod2(facingRight);
    }
}

