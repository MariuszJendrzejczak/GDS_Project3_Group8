using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolPoints;
    private enum StartingState { patrol, attack}
    [SerializeField] StartingState startingState;
    private StateMachine stateMachine;
    [SerializeField] LayerMask playerLeyerMask;
    [SerializeField] [Range(3f, 20f)] private float raycastDistance;

    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.Add("patrol", new EnemyPatrolState());
        stateMachine.Add("attack", new EnemyAttackState());
    }
    private void Start()
    {
        stateMachine.Change(startingState.ToString(), this, stateMachine, patrolPoints, playerLeyerMask, raycastDistance, null);
    }
    private void Update()
    {
        stateMachine.Update();
    }
    public void Shoot()
    {
        Debug.Log("Shoot");
    }
}


public class EnemyStateFields
{
    protected StateMachine stateMachine;
    protected List<object> stateAgrs;
    protected Enemy enemy;
    protected List<Transform> patrolPoints;
    protected float step = 0.1f;
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
        }
        if (enemy.transform.position.x < target.x)
        {
            facing = Faceing.right;
        }
    }

    protected void RaycastMethod()
    {
        switch (facing)
        {
            case Faceing.left:
                hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.left, raycastDistance, playerLayerMask);
                Debug.DrawRay(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.left * raycastDistance, Color.green, 0.1f);
                break;

            case Faceing.right:
                hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.right, raycastDistance, playerLayerMask);
                Debug.DrawRay(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.right * raycastDistance, Color.green, 0.1f);
                break;
        }
    }
}

