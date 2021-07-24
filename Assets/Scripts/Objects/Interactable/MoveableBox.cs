using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableBox : MonoBehaviour, IInteractable
{
    private bool moveMe = false;
    private Rigidbody2D rigidbody;
    private PlayerController player;
    private float handleInpusHorizontal;
    [SerializeField] private Transform myParent;
    public void Interact()
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
            EventBroker.InteractWithObject += Interact;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<PlayerController>();
            EventBroker.InteractWithObject -= Interact;
        }
    }
   /* private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.InteractWithObject -= Interact;
        }
    }*/
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
    }
}
