using UnityEngine;

public class ShwitchOnCollision : MonoBehaviour
{
    [SerializeField] private bool killSelf;
    [SerializeField] private string collisonTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == collisonTag)
        {
            collision.GetComponent<IInteractable>().Interact();
            if(killSelf)
            {
                Destroy(this.gameObject);
            }
        }
    }
}