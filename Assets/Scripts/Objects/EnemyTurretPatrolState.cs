using UnityEngine;
public class EnemyTurretPatrolState : EnemyStateFields, IState
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
        if (hitInfo.collider != null)
        {
            if (hitInfo.transform.tag == "Player")
            {
                stateMachine.Change("attack", enemy, stateMachine, null, playerLayerMask, raycastDistance, hitInfo, rayCastOffsetX, rayCastOffsetY, null);
                Debug.Log(hitInfo.collider.name);
            }
        }
    }
}

