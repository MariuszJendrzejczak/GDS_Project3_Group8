using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFields
{
    protected StateMachine stateMachine;
    protected List<object> stateAgrs;
    protected Enemy enemy;
    protected List<Transform> patrolPoints;
    protected float step = 0.015f;
    protected LayerMask playerLayerMask;
    protected float rayCastOffsetX, rayCastOffsetY;
    protected enum Faceing { left, right}
    protected Faceing facing;
    protected bool facingRight;
    protected Vector2 target;
    protected float raycastDistance;
    protected RaycastHit2D hitInfo;
    protected float localScaleXbase, localScaleXrevers;

    protected void AddParmsToVaribles(params object[] args)
    {
        enemy = (Enemy)args[0];
        stateMachine = (StateMachine)args[1];
        patrolPoints = (List<Transform>)args[2];
        playerLayerMask = (LayerMask)args[3];
        raycastDistance = (float)args[4];
        if (args[5] != null)
        {
            hitInfo = (RaycastHit2D)args[5];
        }
        rayCastOffsetX = (float)args[6];
        rayCastOffsetY = (float)args[7];
        if(args[8] != null)
        {
            facing = (Faceing)args[8];
        }
        if(args[9] != null)
        {
            step = (float)args[9];
        }

        localScaleXbase = enemy.transform.localScale.x;
        localScaleXrevers = enemy.transform.localScale.x * -1f;
    }
    protected void FacingCheck() // old method still used
    {
        if (enemy.transform.position.x > target.x)
        {
            facing = Faceing.left;
            enemy.EnemyFaceing = Enemy.Faceing.left;
        }
        else if (enemy.transform.position.x < target.x)
        {
            facing = Faceing.right;
            enemy.EnemyFaceing = Enemy.Faceing.right;
        }
    }

    protected bool CheckIfFaceingRight() // new method fixing fliping pawn problem
    {
        if(enemy.transform.position.x > target.x)
        {
            facingRight = false;
            return false;
        }
        else if (enemy.transform.position.x < target.x)
        {
            facingRight = true;
            return true;
        }
        return true;
    }

    protected void FlipMethod() // old method still used
    {
        switch (facing)
        {
            case Faceing.left:
                enemy.transform.localScale = new Vector2(localScaleXbase, enemy.transform.localScale.y);
                break;

            case Faceing.right:
                enemy.transform.localScale = new Vector2(localScaleXrevers, enemy.transform.localScale.y);
                break;
        }
    }
    protected void RaycastMethod() // old method still used
    {
        switch (facing)
        {
            case Faceing.left:
                hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x - rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.left, raycastDistance, playerLayerMask);
                Debug.DrawRay(new Vector2(enemy.transform.position.x - rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.left * raycastDistance, Color.green, 0.1f);
                break;

            case Faceing.right:
                hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x + rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.right, raycastDistance, playerLayerMask);
                Debug.DrawRay(new Vector2(enemy.transform.position.x + rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.right * raycastDistance, Color.green, 0.1f);
                break;
        }
    }
    protected void RaycastMethod2(bool facingRight) // new method fixing fliping pawn problem
    {
        if (facingRight)
        {
            hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x + rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.right, raycastDistance, playerLayerMask);
            Debug.DrawRay(new Vector2(enemy.transform.position.x + rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.right * raycastDistance, Color.green, 0.1f);
        }
        else
        {
            hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x - rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.left, raycastDistance, playerLayerMask);
            Debug.DrawRay(new Vector2(enemy.transform.position.x - rayCastOffsetX, enemy.transform.position.y + rayCastOffsetY), Vector2.left * raycastDistance, Color.green, 0.1f);
        }
    }
}

