using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledder : MonoBehaviour, IInteractable
{
    PlayerController player;
    [SerializeField] BoxCollider2D coliderToIngnore;
    bool climbing = false;
    public void Interact()
    {
        if(climbing == false)
        {
            player.GetInteractableObject(this.gameObject);
            player.ChangeState("climb");
            climbing = true;
            if (coliderToIngnore != null)
            {
                coliderToIngnore.enabled = false;
            }
        }
        else if (climbing == true)
        {
            if (coliderToIngnore != null)
            {
                coliderToIngnore.enabled = true;
            }
            climbing = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.GetComponent<PlayerController>();
            EventBroker.InteractWithObject += Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            EventBroker.InteractWithObject -= Interact;
            if (coliderToIngnore != null)
            {
                coliderToIngnore.enabled = true;
            }
        }
    }
}
