using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_Lockpicking : MonoBehaviour
{
    [SerializeField]
    private GameObject pogo;
    private PlayerMovementState state = PlayerMovementState.idle;

    [SerializeField]
    private Transform _neutralPosition;
    [SerializeField]
    private Transform _chargingPosition;
    [SerializeField]
    private Transform _goal;

    [SerializeField]
    private float charge;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case PlayerMovementState.moving:
                break;
            case PlayerMovementState.charging:
                _handleChargingState();
                break;
            case PlayerMovementState.launching:
                _handleLaunchingState();
                break;
            case PlayerMovementState.idle:
                _handleIdleState();
                break;
        }
    }

    private void _handleChargingState() 
    {
        transform.position = _chargingPosition.position;
    }
    private void _handleLaunchingState()
    {
        transform.position = _neutralPosition.position;
        // move to the right? could do on jump
        if (pogo.GetComponent<Rigidbody>().velocity.magnitude < .05f)
        {
            state = PlayerMovementState.idle;
        }
    }
    private void _handleIdleState()
    {
        transform.position = _neutralPosition.position;
    }

    public void OnPogo(InputValue value)
    {
        float input = value.Get<float>();
        
        Debug.Log(input);

        Debug.Log(state);
       
        if (input == 1)
        {
            if (state == PlayerMovementState.idle)
            {
                state = PlayerMovementState.charging;
            }
        }
        else if (input == 0)
        {
            if (state == PlayerMovementState.charging)
            {
                state = PlayerMovementState.launching;
                pogo.GetComponent<Rigidbody>().AddForce(Vector3.right * charge, ForceMode.Impulse);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == PlayerMovementState.launching)
        {
            if (collision.gameObject.tag != "Goal")
            {
                state = PlayerMovementState.idle;
            }
            else
            {
                // win or next level
                //collision.gameObject.GetComponent<Goal>().complete;
            }
        }
    }
}
