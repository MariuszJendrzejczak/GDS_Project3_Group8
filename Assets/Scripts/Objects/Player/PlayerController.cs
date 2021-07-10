using System;
using System.Collections;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    public float PushSpeed { get { return pushSpeed; } }

    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Animator animator;
    private StateMachine stateMachine;
    private GameObject interactableObject;
    private enum Facing { right, left }
    private Facing faceing;
    private enum Armed { none, pistol }
    [SerializeField] private Armed armed = Armed.none;
    [SerializeField] private LayerMask platforemLayerMask;
    [SerializeField] private GameObject lighter;
    [SerializeField] [Range(1f, 5f)] private float speed;
    [SerializeField] [Range(1f, 5f)] private float pushSpeed;
    [SerializeField] [Range(10f, 20f)] private float jumpForce;
    [SerializeField] [Range(1f, 10f)] private float climbLedderSpeed;
    [Range(0.01f, 0.2f)] public float climbEdgeSpeed;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] [Range(1f, 10f)] private float bulletSpeed;
    [SerializeField] [Range(0, 5f)] private float bulletXOffset, bulletYOffset;
    bool cooldown = false;
    [SerializeField][Range(0.1f, 3f)] float colddownTime;

    public event Action InteractWithObject;

    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.Add("idle", new PlayerIdleState());
        stateMachine.Add("walk", new PlayerWalkState());
        stateMachine.Add("jump", new PlayerJumpState());
        stateMachine.Add("verjump", new PlayerVerticalJumpState());
        stateMachine.Add("hang", new PlayerHangingEgdeState());
        stateMachine.Add("climb", new PlayerClimbingState());
        stateMachine.Add("climbdown", new PlayerClimbinDownState());
        stateMachine.Add("push", new PlayerPushState());
        
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        stateMachine.Change("idle", this);
        PlayerArmedAndUnarmedSpriteSwitch();
    }

    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        Torch();
        PlayerInteract();
        Shoot();
        Debug.Log(stateMachine.currentStateId);
    }
    private void Movement(float move, float speed)
    {
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
    private void PlayerInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InteractWithObject != null)
            {
                InteractWithObject();
            }
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
        RaycastHit2D rayCastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 1f, platforemLayerMask);
        Color rayColor;
        float extraHightText = 1f;

        if(rayCastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x, 0), Vector2.down, rayColor, collider.bounds.extents.y + extraHightText);
        Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x, 0), Vector2.down, rayColor, collider.bounds.extents.y + extraHightText);
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
        if (cooldown == false)
        {

            if (Input.GetKeyDown(KeyCode.L) && armed == Armed.pistol)
            {
                Debug.Log("Shoot");
                bool shootLeftBool = true;
                var offset = new Vector2(transform.position.x + bulletXOffset, transform.position.y + bulletYOffset);
                switch (faceing)
                {
                    case Facing.left:
                        offset = new Vector2(transform.position.x + (bulletXOffset * -1f), transform.position.y + bulletYOffset);
                        shootLeftBool = true;
                        break;
                    case Facing.right:
                        shootLeftBool = false;
                        break;
                }

                GameObject projectile = Instantiate(bulletPrefab, offset, Quaternion.identity);
                var movement = projectile.GetComponent<HorizontalProjectileMovement>();
                movement.UpdateShootTo(shootLeftBool);
                StartCoroutine(Cooldown(colddownTime));
                cooldown = true;
            }
        }
        
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
    private IEnumerator Cooldown(float value)
    {
        yield return new WaitForSeconds(value);
        cooldown = false;
    }
}
