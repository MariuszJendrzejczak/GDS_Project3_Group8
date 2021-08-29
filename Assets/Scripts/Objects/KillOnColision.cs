using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnColision : MonoBehaviour
{
    [SerializeField] private bool killCollidedObject;
    [SerializeField] private bool killSelf;
    [SerializeField] private string collisionTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == collisionTag)
        {
            if (killCollidedObject)
            {
                collision.GetComponent<IDestroyAble>().Death();
            }
            if (killSelf)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
