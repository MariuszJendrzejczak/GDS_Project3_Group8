using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDestroyAble, IRespawnBool
{
    public Faceing EnemyFaceing
    {
        get { return facing; }
        set { facing = value; }
    }
 
    protected StateMachine stateMachine;
    [SerializeField] protected LayerMask playerLeyerMask;
    [SerializeField] [Range(3f, 20f)] protected float raycastDistance;
    [SerializeField] [Range(0f, 5f)] protected float rayCastOffsetX, rayCastOffsetY;
    [SerializeField] protected GameObject proyectilePrefab;
    [SerializeField] PoolingObject bulletsPool;
    [SerializeField] [Range(0, 5f)] private float bulletXOffset, bulletYOffset;
    [SerializeField] private GameObject raycastLight;
    private bool cooldown = false;
    [SerializeField] [Range(0.5f, 5f)] private float cooldownTime;
    public enum Faceing { left, right }
    [SerializeField] protected Faceing facing;
    private IRespawnAble respawn;
    protected bool respawned = false;
    public Animator animator;
    [SerializeField][Range(1f, 5f)] protected float patrolSpeed;
    protected GameObject player;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.Add("empty", new EmptyState());
    }
    protected virtual void Start()
    {
        respawn = GetComponent<IRespawnAble>();
        animator = GetComponent<Animator>();
    }
    protected virtual void Update()
    {
        stateMachine.Update();
        stateMachine.HandleInput();
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
            //GameObject projectile = Instantiate(proyectilePrefab, offset, Quaternion.identity);
            GameObject projectile = bulletsPool.GetPooledObject();
            projectile.transform.position = offset;
            projectile.SetActive(true);
            projectile.GetComponent<HorizontalProjectileMovement>().UpdateShootTo(shootLeftBool);
            cooldown = true;
            if (animator != null)
            {
                animator.SetTrigger("shoot");
            }
            StartCoroutine(Cooldown(cooldownTime));
            EventBroker.CallEnemyPlaySfx("enemyshoot");
        }
    }

    public void Shoot2(bool facingRight)
    {
        if (cooldown == false)
        {
            var offset = new Vector2(transform.position.x + bulletXOffset, transform.position.y + bulletYOffset);
            if (facingRight == false)
            {
                offset = new Vector2(transform.position.x + (bulletXOffset * -1f), transform.position.y + bulletYOffset);
            }
            //GameObject projectile = Instantiate(proyectilePrefab, offset, Quaternion.identity);
            GameObject projectile = bulletsPool.GetPooledObject();
            projectile.transform.position = offset;
            projectile.SetActive(true);
            projectile.GetComponent<HorizontalProjectileMovement>().UpdateShootTo(facingRight);
            cooldown = true;
            if (animator != null)
            {
                animator.SetTrigger("shoot");
            }
            StartCoroutine(Cooldown(cooldownTime));
            EventBroker.CallEnemyPlaySfx("enemyshoot");
        }
    }

    protected void OnPlayerDeath()
    {
        stateMachine.Change("empty");
    }
    protected IEnumerator Cooldown(float value)
    {
        yield return new WaitForSeconds(value);
        cooldown = false;
    }

    public void Death()
    {
        EventBroker.PlayerDeath -= OnPlayerDeath;
        respawn.OnMyDeath();
        raycastLight.SetActive(false);
        StartCoroutine(DeathRutine());
    }
    public void ChangeRespawnedBool()
    {
        respawned = true;
    }
    IEnumerator DeathRutine()
    {
        stateMachine.Change("empty");
        animator.SetTrigger("death");
        EventBroker.CallEnemyPlaySfx("ememydeath");
        yield return new WaitForSeconds(3f);
        this.gameObject.SetActive(false);
    }
    public void Respawn(Vector2 value)
    {
        animator.SetTrigger("respawn");
        transform.position = value;
        raycastLight.SetActive(true);
    }

    protected void GetBulletsPool(PoolingObject pool)
    {
        bulletsPool = pool;
    }
}

