using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBox : InteractableObject, IInteractable
{
    private bool moveMe = false;
    [SerializeField] private LayerMask platformLayerMask;
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private float handleInpusHorizontal;
    [SerializeField] private Transform myParent;
    public override void Interact()
    {
        if(moveMe == false)
        {
            Debug.Log("Interact");
            moveMe = true;
            player.ChangeState("push");
            transform.SetParent(player.transform);
            rigidbody.gravityScale = 0;
            SetLayers(0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();
        if (collision.tag == "Player")
        {
            EventBroker.InteractWithObject += Interact;
            EventBroker.CallUpdateTipText(tipText);
        }
    }
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EventBroker.CallUpdateTipText("");
            EventBroker.InteractWithObject -= Interact;
        }
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (moveMe && Input.GetKeyDown(KeyCode.E))
        {
            RealeseBox();
        }
        if (moveMe && IsGrounded() == false)
        {
            RealeseBox();
        }
        if (moveMe && Input.GetKeyUp(KeyCode.A) || moveMe && Input.GetKeyUp(KeyCode.D))
        {
            RealeseBox();
            EventBroker.InteractWithObject += Interact;
        }
    }
    public void RealeseBox()
    {
        moveMe = false;
        player.ChangeState("idle");
        transform.SetParent(myParent);
        rigidbody.gravityScale = 1;
        SetLayers(8);
        EventBroker.InteractWithObject -= Interact;
    }
    private void SetLayers(int value)
    {
        gameObject.layer = value;
        transform.GetChild(0).gameObject.layer = value;
    }
    public bool IsGrounded()
    {
        RaycastHit2D rayCastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.75f, platformLayerMask);
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

        Debug.DrawRay(collider.bounds.center + new Vector3(collider.bounds.extents.x, 0), Vector2.down, rayColor, collider.bounds.extents.y + extraHightText);
        Debug.DrawRay(collider.bounds.center - new Vector3(collider.bounds.extents.x, 0), Vector2.down, rayColor, collider.bounds.extents.y + extraHightText);
        Debug.DrawRay(collider.bounds.center - new Vector3(0, collider.bounds.extents.y), Vector2.right, rayColor, collider.bounds.extents.x);

        return rayCastHit.collider != null;
    }
}
