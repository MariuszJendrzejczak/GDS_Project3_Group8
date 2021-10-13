using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicParalax : MonoBehaviour
{
    [SerializeField][Range(0, 5)] private float speed;
    [SerializeField] private float horizontalInputValue;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector2(speed * Time.deltaTime, 0)); // w prawo
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector2(-1f * speed * Time.deltaTime, 0)); // w lewo
        }
    }
}
