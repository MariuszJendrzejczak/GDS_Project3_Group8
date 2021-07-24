using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDestroyAble
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
        stateMachine.Add("empty", new EmptyState());
    }
    private void Start()
    {
        stateMachine.Change(startingState.ToString(), this, stateMachine, patrolPoints, playerLeyerMask, raycastDistance, null);
        EventBroker.PlayerDeath += OnPlayerDeath;
    }
    private void Update()
    {
        Debug.Log(facing);
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
                Debug.Log("EnemyShootFaceing LEFT!");
                offset = new Vector2(transform.position.x + (bulletXOffset * -1f), transform.position.y + bulletYOffset);
                shootLeftBool = true;
            }
            else if (facing == Faceing.right)
            {
                Debug.Log("EnemyShootFaceing Right!");
                shootLeftBool = false;
            }
            GameObject projectile = Instantiate(proyectilePrefab, offset, Quaternion.identity);
            projectile.GetComponent<HorizontalProjectileMovement>().UpdateShootTo(shootLeftBool);
            cooldown = true;
            StartCoroutine(Cooldown(cooldownTime));
        }
    }

    private void OnPlayerDeath()
    {
        stateMachine.Change("empty");
    }
    private IEnumerator Cooldown(float value)
    {
        yield return new WaitForSeconds(value);
        cooldown = false;
    }

    public void Death()
    {
        EventBroker.PlayerDeath -= OnPlayerDeath;
        Destroy(this.gameObject);
    }
}

