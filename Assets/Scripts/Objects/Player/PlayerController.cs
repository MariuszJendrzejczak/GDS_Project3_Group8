using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    BoxCollider2D collider;
    Animator animator;
    StateMachine stateMachine;

    [SerializeField] LayerMask platforemLayerMask;
    [SerializeField] GameObject lighter;
    [SerializeField] [Range(1f, 5f)] float speed;
    [SerializeField] [Range(10f, 30f)] float jumpForce;

    public event Action InteractWithObject;

    private void Awake()
    {
        stateMachine = new StateMachine();
        stateMachine.Add("idle", new PlayerIdleState());
        stateMachine.Add("walk", new PlayerWalkState());
        stateMachine.Add("jump", new PlayerJumpState());
        stateMachine.Add("hang", new PlayerHangingEgdeState());
        stateMachine.Add("climb", new PlayerClimbingState());
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
        Debug.Log(stateMachine.currentStateId);
    }
    private void Movement(float move)
    {
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
       

    }
    private void HorizontalTurn(float direction)
    {
        if (direction < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
        else if (direction > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
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

    class PlayerIdleState : IState
    {
        PlayerController player;
        float horizontalInputValue;
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            player.animator.SetTrigger("idle");
           horizontalInputValue = Input.GetAxis("Horizontal");
            if (horizontalInputValue != 0)
            {
                player.stateMachine.Change("walk", player);
            }

            if (player.IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                float directionVelocity = horizontalInputValue;
                player.stateMachine.Change("jump", player, directionVelocity);
            }
        }

        public void Update()
        {
            player.Movement(horizontalInputValue);
            player.HorizontalTurn(horizontalInputValue);
            player.Jump();
        }
    }
    class PlayerWalkState : IState
    {
        PlayerController player;
        float horizontalInputValue;
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            horizontalInputValue = Input.GetAxis("Horizontal");
            if(horizontalInputValue == 0)
            {
                player.stateMachine.Change("idle", player);
            }
            if (player.IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                float directionVelocity = horizontalInputValue;
                player.stateMachine.Change("jump", player, directionVelocity);
            }
        }

        public void Update()
        {
            player.animator.SetTrigger("run");
            player.Movement(horizontalInputValue);
            player.HorizontalTurn(horizontalInputValue);
            player.Jump();
        }
    }
    class PlayerJumpState : IState
    {
        PlayerController player;
        float horizintalInputValue;
        public void Enter(params object[] args)
        {
            player = (PlayerController)args[0];
            horizintalInputValue = (float)args[1];
            //player.animator.SetTrigger("jump");
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
            if (player.IsGrounded() && horizintalInputValue == 0)
            {
                player.stateMachine.Change("idle", player);
            }
            else if (player.IsGrounded() && horizintalInputValue != 0)
            {
                player.stateMachine.Change("walk", player);
            }
        }

        public void Update()
        {
            player.Movement(horizintalInputValue);
            player.Jump();
        }
    }
    class PlayerHangingEgdeState : IState
    {
        public void Enter(params object[] args)
        {
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
        }

        public void Update()
        {
        }
    }
    class PlayerClimbingState : IState
    {
        public void Enter(params object[] args)
        {
        }

        public void Exit()
        {
        }

        public void HandleInput()
        {
        }

        public void Update()
        {
        }
    }
}
