using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject followTarget;
    public float moveSpeed;

    private void Update()
    {
        if (followTarget != null)
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.transform.position,
                Time.deltaTime * moveSpeed);
        }
    }
}