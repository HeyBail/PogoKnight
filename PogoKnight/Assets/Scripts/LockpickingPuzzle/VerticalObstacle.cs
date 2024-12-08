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
    private float _speed;
    public float SpeedProp
    {
        get { return _speed; }
        set { _speed = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (obstacle.transform.position.y > _upperBound.position.y)
        {
            direction = -1;
        }
        else if (obstacle.transform.position.y < _lowerBound.position.y)
        {
            direction = 1;
        }
        obstacle.transform.position += obstacle.transform.up * Time.deltaTime * direction * _speed;
    }
}
