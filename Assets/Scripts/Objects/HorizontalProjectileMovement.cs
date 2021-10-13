using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalProjectileMovement : MonoBehaviour
{
    private enum ShootTo { right, left }
    private ShootTo shootTo;
    [SerializeField][Range(1f, 25f)] private float bulletSpeed = 1f;
    [SerializeField] [Range(1f, 5f)] private float switchoffafterSeconds = 3f;

    private void OnEnable()
    {
        StartCoroutine(Switchoff(switchoffafterSeconds));
    }
    private void Start()
    {
        if(shootTo == ShootTo.right)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

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

    private IEnumerator Switchoff(float sec)
    {
        yield return new WaitForSeconds(sec);
        this.gameObject.SetActive(false);
    }
}
