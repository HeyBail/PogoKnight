using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerOscilator : MonoBehaviour
{
    public float moveDistance = .75f;  // Maximum distance to oscillate
    public float speed = 0.2f;       // Speed of oscillation

    private float startZ;            // Starting Z position
    private float minZ;              // Minimum Z position
    private float maxZ;              // Maximum Z position
    private float direction = 1f;    // Current direction of movement

    // Start is called before the first frame update
    void Start()
    {
        // Record the starting position
        startZ = transform.localPosition.z;
        minZ = startZ - moveDistance;
        maxZ = startZ + moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object along the Z axis
        transform.localPosition += Vector3.forward * direction * speed * Time.deltaTime;

        // Reverse direction if bounds are exceeded
        if (transform.localPosition.z >= maxZ)
        {
            direction = -1f; // Move backward
        }
        else if (transform.localPosition.z <= minZ)
        {
            direction = 1f; // Move forward
        }
    }
}
