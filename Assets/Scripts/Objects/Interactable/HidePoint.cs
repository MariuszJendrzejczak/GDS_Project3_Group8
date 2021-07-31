using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePoint : MonoBehaviour, IInteractable
{
    private PlayerController player;
    [SerializeField] SpriteRenderer renderer;
    public void Interact()
    {
        renderer.sortingOrder = 100;
        player.HideMethod(renderer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventBroker.InteractWithObject += Interact;
            player = collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventBroker.InteractWithObject -= Interact;
        }
    }
}
