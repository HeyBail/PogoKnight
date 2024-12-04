using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class EnemySlime : MonoBehaviour
{
    private SlimeState state = SlimeState.idle;

    private Vector3 _velocity;

    private LayerMask groundMask;
    private bool _isGrounded;

    //jumping / moving
    [SerializeField]
    private float _jumpHeight = 4f;
    [SerializeField]
    private float _maxJumpHeight = 4f;
    [SerializeField]
    private float _chargeSpeed = 1.5f;

    private LineRenderer lineRenderer;

    [SerializeField]
    GameObject player;

    [SerializeField]
    private float _chargingDistance = 10f;
    [SerializeField]
    private float _jumpAwayDistance = 7f;

    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = .25f;
        lineRenderer.endWidth = .25f;
        lineRenderer.material.color = Color.cyan;
        lineRenderer.enabled = false;

        groundMask = LayerMask.GetMask("Ground");    
    }

    private void FixedUpdate()
    {
        _isGrounded = isGrounded();

        if (checkLanding())
        {
            handleLanding();
        }


        switch (state)
        {
            case SlimeState.charging:
                _handleChargingState();
                break;
            case SlimeState.launching:
                _handleLaunchingState();
                break;
            case SlimeState.idle:
                _handleIdleState();
                break;
        }
    }

    //handlers
    private void _handleChargingState()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up);

        Vector3 center = gameObject.transform.position;
        lineRenderer.SetPosition(0, center);

        Vector3 end;

        end = center + new Vector3(-1 * transform.forward.x * _jumpHeight, _jumpHeight, transform.forward.z * _jumpHeight * -1);

        Debug.Log("x transform: " + transform.forward.x);
        Debug.Log("z transform: " + transform.forward.y);

        lineRenderer.SetPosition(1, end);

        _jumpHeight += Time.deltaTime * _chargeSpeed;
        _jumpHeight = Mathf.Clamp(_jumpHeight, 0, _maxJumpHeight);

        Debug.Log(_jumpHeight);

        if (Vector3.Distance(player.transform.position, transform.position) < _jumpAwayDistance)
        {
            Debug.Log("jump away");

            _rigidbody.AddForce(new Vector3(-1 * transform.forward.x * Mathf.Sqrt(_jumpHeight), _jumpHeight, -1 *  transform.forward.z * Mathf.Sqrt(_jumpHeight)), ForceMode.Impulse);
            state = SlimeState.launching;
        }
    }

    private void _handleLaunchingState()
    {
        //Debug.Log("jummping, looking for bounce");
    }

    private void _handleIdleState()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up);

        if (Vector3.Distance(player.transform.position, transform.position) < _chargingDistance)
        {
            Debug.Log("switch to charging");

            lineRenderer.enabled = true;
            state = SlimeState.charging;
        }
    }

    //misc private methods
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.2f, groundMask) & _velocity.y <= 0;
    }

    private bool checkLanding()
    {
        return state == SlimeState.launching & _isGrounded;
    }

    private void handleLanding()
    {
        state = SlimeState.idle;
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        lineRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HITTT");

        if (other.gameObject.tag == "TankWall")
        {
            Debug.Log("HITTT WALLL");
            Debug.Log("Velocity Before: " + _rigidbody.velocity);

            Vector3 closestPoint = other.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - closestPoint).normalized;



            Vector3 incomingVelocity = _rigidbody.velocity;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

            _rigidbody.velocity = reflectedVelocity;


            Debug.Log("Velocity After: " + _rigidbody.velocity);
        }
    }
}
