using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBox : InteractableObject, IInteractable
{
    private bool moveMe = false;
    private Rigidbody2D rigidbody;
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
        }
        else if (moveMe)
        {
            moveMe = false;
            player.ChangeState("idle");
            transform.SetParent(myParent);
            EventBroker.InteractWithObject -= Interact;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();
        if (collision.tag == "Player")
        {
            EventBroker.InteractWithObject += Interact;
            EventBroker.CallUpdateTipText(tipText);
        }
    }*/
    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EventBroker.CallUpdateTipText("");
        }
    }
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
    }
}
