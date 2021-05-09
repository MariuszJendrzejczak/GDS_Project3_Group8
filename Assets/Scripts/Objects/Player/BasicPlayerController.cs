using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject lighter;
    private Rigidbody2D rigidbody;
    private Animator animator;
    [SerializeField] [Range(1f, 5f)] float speed;
    [SerializeField] [Range(10f, 30f)] float jumpForce;

    public event Action InteractWithObject;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Torch();
        PlayerInteract();
    }
    private void Movement()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(move * speed, rigidbody.velocity.y);
        if(move < 0)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            animator.SetTrigger("run");
        }
        else if (move > 0)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            animator.SetTrigger("run");
        }
        else
        {
            animator.SetTrigger("idle");
        }

    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }
    private void Torch()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (lighter.active == true)
            {
                lighter.SetActive(false);
            }
            else if (lighter.active == false)
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
}
