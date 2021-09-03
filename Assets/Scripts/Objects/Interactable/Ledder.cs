using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledder : InteractableObject, IInteractable
{
    [SerializeField] BoxCollider2D coliderToIngnore;
    bool climbing = false;
    public override void Interact()
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

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if(collision.tag == "Player")
        {
            if (coliderToIngnore != null)
            {
                coliderToIngnore.enabled = true;
            }
        }
    }
}
