using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectTransform : MonoBehaviour
{
    [SerializeField] Transform objectToFollow;
    [SerializeField] private Vector3 offset;
    [SerializeField][Range(2, 15)] private float smoothSpeed = 0.125f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objectToFollow != null)
        {
            FollowObject();
        }
    }

    public void SetObjectToFollow(Transform value)
    {
        objectToFollow = value;
    }

    private void FollowObject()
    {
        Vector3 desiredPosition = objectToFollow.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
