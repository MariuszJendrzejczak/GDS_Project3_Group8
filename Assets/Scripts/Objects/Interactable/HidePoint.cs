using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePoint : MonoBehaviour, IInteractable
{
    private PlayerController player;
    [SerializeField] SpriteRenderer hideSquare;
    public void Interact()
    {
        hideSquare.enabled = true;
        player.HideMethod(hideSquare);
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
