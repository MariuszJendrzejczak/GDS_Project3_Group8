using System;
using System.Collections;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    BoxCollider2D collider;
    Animator animator;
    StateMachine stateMachine;
    GameObject interactableObject;
    enum Facing {right, left}
    private Facing faceing;

    [SerializeField] LayerMask platforemLayerMask;
    [SerializeField] GameObject lighter;
    [SerializeField] [Range(1f, 5f)] float speed;
    [SerializeField] [Range(10f, 20f)] float jumpForce;
    [SerializeField] [Range(10f, 20f)] float jumpForceVertical;
    [SerializeField] [Range(1f, 10f)] float climbLedderSpeed;
    [Range(0.01f, 0.2f)] public float climbEdgeSpeed;

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
        
    }
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        stateMachine.Change("idle", this);
    }

    void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
        Torch();
        PlayerInteract();

    }
    private void Movement(float move)
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
}
