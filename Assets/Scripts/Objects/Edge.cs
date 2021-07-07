using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    PlayerController player;
    public enum FaceingRequirement { right, left }
    public FaceingRequirement facingRequirement;
    public List<Transform> transformList;
    public BoxCollider2D colliderToIngrre;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<PlayerController>();
            player.GetInteractableObject(this.gameObject);
            if (player.IsGrounded() == false)
            {
                player.ChangeState("hang");
            }

  //          colliderToIngrre.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //player.GetInteractableObject(null);
 //       colliderToIngrre.enabled = true;
    }
    public void ClimbDown()
    {
        player.transform.position = Vector2.MoveTowards(player.transform.position, transformList[0].position, player.climbEdgeSpeed);
        player.ChangeState("hang");
    }
}
