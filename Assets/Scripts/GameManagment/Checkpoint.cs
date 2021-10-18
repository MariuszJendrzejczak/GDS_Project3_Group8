using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private BoxCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            EventBroker.CallCheckPointReached(this.gameObject);
            collider.gameObject.SetActive(false);
        }
    }
}
