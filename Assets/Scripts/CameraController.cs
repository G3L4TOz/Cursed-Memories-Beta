using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    public float minX, maxX, minY, maxY;

    void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, desiredPosition.z);
    }
} 

