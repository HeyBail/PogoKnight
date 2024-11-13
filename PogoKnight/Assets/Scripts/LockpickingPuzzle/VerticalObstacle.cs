using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalObstacle : MonoBehaviour
{
    [SerializeField]
    private Transform _upperBound;
    [SerializeField]
    private Transform _lowerBound;

    [SerializeField]
    private GameObject obstacle;

    [SerializeField]
    private int direction = -1;

    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(obstacle.transform.position, _upperBound.position) < .05f)
        {
            direction = -1;
        }
        else if (Vector3.Distance(obstacle.transform.position, _lowerBound.position) < .05f)
        {
            direction = 1;
        }
        obstacle.transform.position += obstacle.transform.up * Time.deltaTime * direction * speed;
    }
}
