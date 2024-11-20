using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private Vector3 _velocity;

    private CharacterController _characterController;

    private LayerMask groundMask;
    private bool _isGrounded;

    private Vector2 movementInput;
    private Vector2 cameraInput;
    private float _xRotation = 90f;
    private float _mouseSensitivity = 10f;

    public Transform cameraCenter;
    public GameObject playerCamera;

    private PlayerMovementState movementState = PlayerMovementState.moving;


    //jumping / moving
    [SerializeField]
    private float _movementSpeed = 3f;
    [SerializeField]
    private float _airControlSpeed = 1f;
    [SerializeField]
    private float _gravity = -9.81f;
    [SerializeField]
    private float _jumpHeight = 4f;
    [SerializeField]
    private float _maxJumpHeight = 4f;
    [SerializeField]
    private float _chargeSpeed = 1.5f;

    private LineRenderer lineRenderer;

    public delegate void OnInteractChanged();
    public static event OnInteractChanged onInteractChanged;

    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = .25f;
        lineRenderer.endWidth = .25f;
        lineRenderer.material.color = Color.cyan;
        lineRenderer.enabled = false;

        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        groundMask = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        _isGrounded = isGrounded();

        if (checkLanding())
        {
            handleLanding();
        }

    }

    private void Update()
    {
        handleVerticalMovement();

        handleMouseMovement();

        switch (movementState)
        {
            case PlayerMovementState.moving:
                _handleMovingState();
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


    //handlers
    private void _handleMovingState() 
    { 
        //movement stuff
        Vector3 movement = transform.right * movementInput.x + transform.forward * movementInput.y;
        _characterController.Move(movement * _movementSpeed * Time.deltaTime);
    }

    private void _handleChargingState()
    {
        Vector3 center = gameObject.transform.position;
        lineRenderer.SetPosition(0, center);
        Vector3 end = center + transform.TransformDirection(new Vector3(movementInput.x * _jumpHeight, _jumpHeight, movementInput.y * _jumpHeight));
        lineRenderer.SetPosition(1, end);

        // could rotate the player instead / as well

        _jumpHeight += Time.deltaTime * _chargeSpeed;
        _jumpHeight = Mathf.Clamp(_jumpHeight, 0, _maxJumpHeight);
    }

    private void _handleLaunchingState()
    {
        //_velocity.x += // make this movement nicer, continually increase speed when launching using movement keys. maybe keep track of horizontal velocity and keep adding to it
        Vector3 movement = transform.right * movementInput.x + transform.forward * movementInput.y;
        _characterController.Move(movement * _airControlSpeed * Time.deltaTime);
        //not a whole lot to do, maybe mid air rotations
    }

    private void _handleIdleState()
    {
        //maybe bounce a bit? not sure
    }

    private void handleVerticalMovement()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity = Vector3.zero;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void handleMouseMovement()
    {
        //camera stuff
        _xRotation -= cameraInput.y * _mouseSensitivity * Time.deltaTime;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        cameraCenter.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * cameraInput.x * _mouseSensitivity * Time.deltaTime);
    }


    //misc private methods
    private bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.2f, groundMask) & _velocity.y <= 0;
    }

    private bool checkLanding() 
    {
        return movementState == PlayerMovementState.launching & _isGrounded;
    }

    private void handleLanding() 
    {
        movementState = PlayerMovementState.moving;
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        lineRenderer.enabled = false;
    }

    //Movement Input Actions
    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();

        if (movementInput.magnitude == 0 & _isGrounded & movementState == PlayerMovementState.moving)
        {
            movementState = PlayerMovementState.idle;
        }
        else if (movementInput.magnitude == 1 & _isGrounded & movementState == PlayerMovementState.idle)
        {
            movementState = PlayerMovementState.moving;
        }
    }

    public void OnLook(InputValue value)
    {
        cameraInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        float input = value.Get<float>();

        if (!(movementState == PlayerMovementState.charging) & _isGrounded & input == 1)
        {
            movementState = PlayerMovementState.charging;
            _jumpHeight = 0;

            lineRenderer.enabled = true;
        }
        else if ((movementState == PlayerMovementState.charging) & _isGrounded & input == 0)
        {
            movementState = PlayerMovementState.launching;

            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity); //this could all be cleaned up late, but probably wont be
            _velocity.x = movementInput.x * _jumpHeight;
            _velocity.z = movementInput.y * _jumpHeight;
            _velocity = transform.TransformDirection(_velocity);
        }
    }
    public void OnInteract(InputValue value)
    {
        onInteractChanged?.Invoke();
    }
}