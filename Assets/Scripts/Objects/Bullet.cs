using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private enum ShootTo { right, left }
    private ShootTo shootTo;
    private float bulletSpeed = 1f;

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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + " " + collision.tag);
        switch (collision.tag)
        {
            case "Door":
                Destroy(this.gameObject);
                break;
            case "Enemy":
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
                break;
        }
    }
    public void UpdateBulletSpeed(float value)
    {
        bulletSpeed = value;
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
