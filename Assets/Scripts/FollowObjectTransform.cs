using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectTransform : MonoBehaviour
{
    [SerializeField] Transform objectToFollow;
    [SerializeField][Range(-10f, 10f)] private float offsetX, offsetY, offsetZ;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.position.x + offsetX, objectToFollow.position.y + offsetY, objectToFollow.position.z + offsetZ);
    }
}
