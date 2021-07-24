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
    protected enum Faceing { left, right}
    protected Faceing facing;
    protected Vector2 target;
    protected float raycastDistance;
    protected RaycastHit2D hitInfo;

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
    }
    protected void FacingCheck()
    {
        if (enemy.transform.position.x > target.x)
        {
            facing = Faceing.left;
            enemy.EnemyFaceing = Enemy.Faceing.left;

        }
        if (enemy.transform.position.x < target.x)
        {
            facing = Faceing.right;
            enemy.EnemyFaceing = Enemy.Faceing.right;
        }
    }

    protected void RaycastMethod()
    {
        switch (facing)
        {
            case Faceing.left:
                hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.left, raycastDistance, playerLayerMask);
                Debug.DrawRay(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.left * raycastDistance, Color.green, 0.1f);
                enemy.transform.localScale = new Vector2(-0.5f, enemy.transform.localScale.y);
                break;

            case Faceing.right:
                hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.right, raycastDistance, playerLayerMask);
                Debug.DrawRay(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.right * raycastDistance, Color.green, 0.1f);
                enemy.transform.localScale = new Vector2(0.5f, enemy.transform.localScale.y);
                break;
        }
    }
}

