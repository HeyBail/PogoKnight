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
    private float _minJumpHeight = 4f;
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

    [SerializeField]
    private float _minScale = 4f;
    [SerializeField]
    private float _maxScale = 4f;

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

        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), Vector3.up);

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
        Vector3 center = gameObject.transform.position;
        lineRenderer.SetPosition(0, center);

        Vector3 end;

        end = center + new Vector3(-1 * transform.forward.x * _jumpHeight, _jumpHeight, transform.forward.z * _jumpHeight * -1);

        lineRenderer.SetPosition(1, end);


        if (Vector3.Distance(player.transform.position, transform.position) <= _chargingDistance)
        {
            _jumpHeight += Time.deltaTime * _chargeSpeed;
            _jumpHeight = Mathf.Clamp(_jumpHeight, 0, _maxJumpHeight);
        }
        else 
        {
            _jumpHeight -= Time.deltaTime * _chargeSpeed;
            _jumpHeight = Mathf.Clamp(_jumpHeight, 0, _maxJumpHeight);
        }


        _chargeSlimeAnimation();

        if (Vector3.Distance(player.transform.position, transform.position) < _jumpAwayDistance)
        {
            _rigidbody.AddForce(new Vector3(-1 * transform.forward.x * Mathf.Sqrt(_jumpHeight), _jumpHeight, -1 *  transform.forward.z * Mathf.Sqrt(_jumpHeight)), ForceMode.Impulse);
            state = SlimeState.launching;

            _jumpHeight = _minJumpHeight;


            transform.localScale = new Vector3(_maxScale, _maxScale, _maxScale);
        }
    }

    void _chargeSlimeAnimation() 
    {
        float lerpedJumpHeight = Mathf.InverseLerp(_minJumpHeight, _maxJumpHeight, _jumpHeight);

        float newScale = Mathf.Lerp(_minScale, _maxScale, 1 - lerpedJumpHeight);

        transform.localScale = new Vector3(transform.localScale.x, newScale, transform.localScale.z);
    }

    private void _handleLaunchingState()
    {

    }

    private void _handleIdleState()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < _chargingDistance)
        {
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
        if (other.gameObject.tag == "TankWall")
        {
            Vector3 closestPoint = other.ClosestPoint(transform.position);
            Vector3 normal = (transform.position - closestPoint).normalized;

            Vector3 incomingVelocity = _rigidbody.velocity;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

            _rigidbody.velocity = reflectedVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & groundMask) != 0)
        {
            state = SlimeState.idle;
        }
    }
}
