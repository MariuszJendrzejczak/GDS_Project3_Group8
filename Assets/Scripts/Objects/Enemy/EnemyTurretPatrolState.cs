using UnityEngine;
public class EnemyTurretPatrolState : EnemyStateFields, IState
{
    public void Enter(params object[] args)
    {
        AddParmsToVaribles(args);
        if (facing == Faceing.right)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }
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
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.tag == "Player")
            {
                stateMachine.Change("attack", enemy, stateMachine, null, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null, step, facingRight);
                Debug.Log(hitInfo.collider.name);
            }
        }
    }
}

