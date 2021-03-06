using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class PlayerController : MonoBehaviour, IDestroyAble, IMakeInteraction
{
    public float PushSpeed { get { return pushSpeed; } }
    public LayerMask PlatformLayerMask { get { return platforemLayerMask; } }

    public Rigidbody2D rigidbody;
    private CapsuleCollider2D collider;
    [SerializeField] private Animator animator;
    private StateMachine stateMachine;
    private GameObject interactableObject;
    private enum Facing { right, left }
    private Facing faceing;
    private enum Armed { none, pistol }
    private SpriteRenderer hideRenderer;
    private bool hidedPlayer = false;
    [SerializeField] private Armed armed = Armed.none;
    [SerializeField] private LayerMask platforemLayerMask;
    [SerializeField] private GameObject lighter;
    [SerializeField] [Range(1f, 20f)] private float speed;
    [SerializeField] [Range(1f, 20f)] private float pushSpeed;
    [SerializeField] [Range(5f, 40f)] private float jumpForce;
    [SerializeField] [Range(1f, 200f)] private float climbLedderSpeed;
    [Range(0.01f, 0.2f)] public float climbEdgeSpeed;
    [SerializeField] private PoolingObject bulletPool;
    [SerializeField] [Range(1f, 20f)] private float bulletSpeed;
    [SerializeField] [Range(0, 5f)] private float bulletXOffset, bulletYOffset;
    bool cooldown = false;
    [SerializeField] [Range(0.1f, 10f)] float colddownTime;
    [SerializeField] bool godMode = false;
    public bool onElevator = false;

    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.Add("empty", new EmptyState());
        stateMachine.Add("idle", new PlayerIdleState());
        stateMachine.Add("walk", new PlayerWalkState());
        stateMachine.Add("jump", new PlayerJumpState());
        stateMachine.Add("verjump", new PlayerVerticalJumpState());
        stateMachine.Add("hang", new PlayerHangingEgdeState());
        stateMachine.Add("climb", new PlayerClimbingState());
        stateMachine.Add("climbdown", new PlayerClimbinDownState());
        stateMachine.Add("push", new PlayerPushState());
        stateMachine.Add("death", new PlayerDeathState());
        stateMachine.Add("fall", new PlayerFallState());

    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
        stateMachine.Change("idle", this);
        PlayerArmedAndUnarmedSpriteSwitch();
        EventBroker.CallGiveToAllPlayerCharacterControlerRef(GetComponent<PlayerController>());
        EventBroker.CallGiveToAllPlayerCharacterRef(this.gameObject);
        EventBroker.ChangeOnElevatorBoolOnPlayer += ChangeOnElevatorBool;
    }

    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        Torch();
        MakeInteraction();
        Shoot();
        FallCheck();
        QuitGame();
        
        //switched off development tool
        if (Input.GetKeyDown(KeyCode.G))
        {
            GodModeMethod();
        }
    }
    public void GetParamsFromGameManager(params object[] args)
    {
        bulletPool = (PoolingObject)args[0];
    }
    private void GodModeMethod()
    {
        if (godMode == true)
        {
            godMode = false;
        }
        else if (godMode == false)
        {
            godMode = true;
        }
    }
    public void HideMethod(SpriteRenderer value)
    {
        hideRenderer = value;
        transform.tag = "Hide";
        hidedPlayer = true;
        animator.SetTrigger("cover");
    }

    private void UnHideMethod()
    {
        hideRenderer.sortingOrder = 10;
        hideRenderer = null;
        transform.tag = "Player";
        hidedPlayer = false;
    }
    private void Movement(float move, float speed)
    {
        if (hidedPlayer == true && move != 0)
        {
            UnHideMethod();
        }
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
    }
    private void ClimbLedder(float value)
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, value * climbLedderSpeed);
    }
    private void HorizontalTurn(float direction)
    {
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            faceing = Facing.left;
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            faceing = Facing.right;
        }
    }
    private void Jump(float value)
    {
        if (hidedPlayer == true)
        {
            UnHideMethod();
        }
        if (IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, value);
        }
    }

    private void Torch()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (lighter.activeSelf == true)
            {
                lighter.SetActive(false);
            }
            else if (lighter.activeSelf == false)
            {
                lighter.SetActive(true);
            }
        }
    }
    public void MakeInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventBroker.CallInteractWithObject();
        }
    }
    public void GetInteractableObject(GameObject obj)
    {
        interactableObject = obj;
    }

    public void ChangeState(string key)
    {
        stateMachine.Change(key, this, interactableObject);
    }
    public void SetGravityValue(float value)
    {
        rigidbody.gravityScale = value;
    }

    public bool IsGrounded() 
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size * 0.5f, 0f, Vector2.down, 0.75f, platforemLayerMask);
        Color rayColor;
        float extraHightText = 1f;

        if (rayCastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x * 0.5f, 0), Vector2.down, rayColor, collider.bounds.extents.y + extraHightText);
        Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x * 0.5f, 0), Vector2.down, rayColor, collider.bounds.extents.y + extraHightText);
        Debug.DrawRay(collider.bounds.center - new Vector3(0, collider.bounds.extents.y), Vector2.right, rayColor, collider.bounds.extents.x);

        return rayCastHit.collider != null;
    }

    private void PlayerArmedAndUnarmedSpriteSwitch()
    {
        switch (armed)
        {
            case Armed.none:
                // placeing sprite without pistol
                break;
            case Armed.pistol:
                // uplaceing spirte with pistol
                break;
        }
    }

    private void Shoot()
    {
        if (cooldown == false && stateMachine.currentStateId == "idle" && hidedPlayer == false && IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.L) && armed == Armed.pistol)
            {
                Debug.Log("Shoot");
                animator.SetTrigger("shoot");
                EventBroker.CallCharacterPlaySfx("shoot");
                bool shootRightbool = true;
                var offset = new Vector2(transform.position.x + bulletXOffset, transform.position.y + bulletYOffset);
                switch (faceing)
                {
                    case Facing.left:
                        offset = new Vector2(transform.position.x + (bulletXOffset * -1f), transform.position.y + bulletYOffset);
                        shootRightbool = false;
                        break;
                    case Facing.right:
                        shootRightbool = true;
                        break;
                }
                GameObject projectile = bulletPool.GetPooledObject();
                if(projectile != null)
                {
                    projectile.transform.position = offset;
                    projectile.SetActive(true);
                    var movement = projectile.GetComponent<HorizontalProjectileMovement>();
                    movement.UpdateShootTo(shootRightbool);
                }

                cooldown = true;
                StartCoroutine(Cooldown(colddownTime));
                // animator.ResetTrigger("shoot");
            }
        }
    }
    private void FallCheck()
    {
        if (rigidbody.velocity.y < 0 && IsGrounded() == false)
        {
            ChangeState("fall");
        }
    }
    public void LaunchLandCoroutine()
    {
        StartCoroutine(Land());
    }

    public void LaunchDeathCoroutine()
    {
        StartCoroutine(DeathRutine());
    }
    public void Death()
    {
        if (godMode == false)
        {
            ChangeState("death");
        }
    }
    public void Respawn(Vector2 value)
    {
        Debug.Log("RespawnPlayer at: " + value.ToString());
        transform.position = value;
        this.gameObject.SetActive(true);
        ChangeState("idle");
    }
    private void QuitGame()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    
    public void GetElevatorAsParemt(GameObject elevator)
    {
        if (onElevator)
        {
            transform.SetParent(elevator.transform);
        }
    }
    public void SetParrentNull()
    {
        transform.SetParent(null);
    }

    public void ChangeOnElevatorBool(bool value)
    {
        onElevator = value;
    }

private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Edge" && stateMachine.currentStateId == "verjump")
        {
            Edge edge = collision.GetComponent<Edge>();
            if (edge.facingRequirement.ToString() == faceing.ToString())
            {
                stateMachine.Change("hang", this);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ledder" && stateMachine.currentStateId == "climb")
        {
            stateMachine.Change("idle", this);
        }
    }
    private IEnumerator DeathRutine()
    {
        yield return new WaitForSeconds(2f);
    }
    private IEnumerator Cooldown(float value)
    {
        EventBroker.CallCharacterPlaySfxLayer2("cooldown");
        yield return new WaitForSeconds(value);
        cooldown = false;
    }
    private IEnumerator Land()
    {
        animator.SetTrigger("land");
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger("idle");
        ChangeState("idle");
    }
}
 
