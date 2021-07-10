using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Faceing EnemyFaceing
    {
        get { return facing; }
        set { facing = value; }
    }
    [SerializeField] private List<Transform> patrolPoints;
    private enum StartingState { patrol, attack }
    [SerializeField] StartingState startingState;
    private StateMachine stateMachine;
    [SerializeField] LayerMask playerLeyerMask;
    [SerializeField] [Range(3f, 20f)] private float raycastDistance;
    [SerializeField] GameObject proyectilePrefab;
    [SerializeField] [Range(0, 5f)] private float bulletXOffset, bulletYOffset;
    private bool cooldown = false;
    [SerializeField] [Range(0.5f, 5f)] private float cooldownTime;
    public enum Faceing { left, right }
    private Faceing facing;

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
        if (cooldown == false)
        {
            bool shootLeftBool = true;
            var offset = new Vector2(transform.position.x + bulletXOffset, transform.position.y + bulletYOffset);
            if (facing == Faceing.left)
            {
                offset = new Vector2(transform.position.x + (bulletXOffset * -1f), transform.position.y + bulletYOffset);
                shootLeftBool = true;
            }
            else if (facing == Faceing.right)
            {
                shootLeftBool = false;
            }
            GameObject projectile = Instantiate(proyectilePrefab, offset, Quaternion.identity);
            projectile.GetComponent<HorizontalProjectileMovement>().UpdateShootTo(shootLeftBool);
            cooldown = true;
            StartCoroutine(Cooldown(cooldownTime));
        }
    }
    private IEnumerator Cooldown(float value)
    {
        yield return new WaitForSeconds(value);
        cooldown = false;

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
                break;

            case Faceing.right:
                hitInfo = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.right, raycastDistance, playerLayerMask);
                Debug.DrawRay(new Vector2(enemy.transform.position.x, enemy.transform.position.y + 2.5f), Vector2.right * raycastDistance, Color.green, 0.1f);
                break;
        }
    }
}

