using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    float maxDistance;

    [SerializeField]
    float distance;

    [SerializeField]
    LayerMask ignoreLayers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (cameraTransform.position - transform.position).normalized;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, maxDistance, ~ignoreLayers)) 
        {
            distance = hit.distance;

            if (distance > maxDistance) 
            {
                distance = maxDistance;
            }
        }
        Debug.Log(distance);
        cameraTransform.localPosition = new Vector3(0, 0, -distance);
    }
}
