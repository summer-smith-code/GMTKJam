using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobPositionFollower : MonoBehaviour
{
    public Transform targetTransform;
    [HideInInspector]
    public Vector3 offset;
    public float smoothTime = 0.3f;

    Vector3 velocity = Vector3.zero;

    void Update()
    {
        //transform.position = targetTransform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetTransform.position + offset, ref velocity, smoothTime);
    }
}
