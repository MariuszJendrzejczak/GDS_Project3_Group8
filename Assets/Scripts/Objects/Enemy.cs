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
 
    protected StateMachine stateMachine;
    [SerializeField] protected LayerMask playerLeyerMask;
    [SerializeField][Range(3f, 20f)] protected float raycastDistance;
    [SerializeField] [Range(0f, 5f)] protected float rayCastOffsetX, rayCastOffsetY;
    [SerializeField] protected GameObject proyectilePrefab;
    [SerializeField] [Range(0, 5f)] private float bulletXOffset, bulletYOffset;
    private bool cooldown = false;
    [SerializeField] [Range(0.5f, 5f)] private float cooldownTime;
    public enum Faceing { left, right }
    [SerializeField] protected Faceing facing;
    private IRespawnAble respawn;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.Add("empty", new EmptyState());
    }
    protected virtual void Start()
    {
        EventBroker.PlayerDeath += OnPlayerDeath;

        respawn = GetComponent<IRespawnAble>();
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
            GameObject projectile = Instantiate(proyectilePrefab, offset, Quaternion.identity);
            projectile.GetComponent<HorizontalProjectileMovement>().UpdateShootTo(shootLeftBool);
            cooldown = true;
            StartCoroutine(Cooldown(cooldownTime));
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
        this.gameObject.SetActive(false);
    }
}

