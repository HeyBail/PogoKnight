using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Drag the target object or position here
    public float speed = 2f; // Speed of the camera movement
    private bool moveToTarget = false;

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the camera
        initialPosition = transform.position;
    }

    void Update()
    {
        // Move the camera smoothly to the target if moveToTarget is true
        if (moveToTarget)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, speed * Time.deltaTime);

            // Optional: Stop moving when very close to the target
            if (Vector3.Distance(transform.position, target.position) < 0.01f)
            {
                transform.position = target.position; // Snap to target to prevent floating point inaccuracies
                moveToTarget = false; // Stop further movement
            }
        }
    }

    // Call this function to start moving the camera
    public void MoveToTarget()
    {
        moveToTarget = true;
    }

    // Call this function to reset the camera to its initial position
    public void ResetPosition()
    {
        transform.position = initialPosition;
        moveToTarget = false;
    }
}
