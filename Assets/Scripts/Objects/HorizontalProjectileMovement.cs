using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalProjectileMovement : MonoBehaviour
{
    private enum ShootTo { right, left }
    private ShootTo shootTo;
    [SerializeField][Range(1f, 25f)] private float bulletSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        switch (shootTo)
        {
            case ShootTo.left:
                transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);
                break;
            case ShootTo.right:
                transform.Translate(Vector2.right * bulletSpeed * Time.deltaTime);
                break;
        }
    }
    public void UpdateShootTo(bool value)
    {
        if (value)
        {
            shootTo = ShootTo.left;
        }
        else if (value == false)
        {
            shootTo = ShootTo.right;
        }
    }
}
