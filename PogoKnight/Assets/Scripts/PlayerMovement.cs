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

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector2 movementInput;
    private Vector2 cameraInput;
    private float _xRotation = 90f;
    private float _mouseSensitivity = 10f;

    public Transform cameraCenter;
    public GameObject camera;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        groundMask = LayerMask.GetMask("Ground");
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        cameraInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (_isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private bool isGrounded() 
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.2f, groundMask);
    }

    private void FixedUpdate()
    {
        _isGrounded = isGrounded();
    }

    private void Update()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }
        _velocity.y += gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        //movement stuff
        Vector3 movement = transform.right * movementInput.x + transform.forward * movementInput.y;
        _characterController.Move(movement * speed * Time.deltaTime);

        //camera stuff
        _xRotation -= cameraInput.y * _mouseSensitivity * Time.deltaTime;
        _xRotation = Mathf.Clamp(_xRotation, 0f, 90f);
        cameraCenter.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * cameraInput.x * _mouseSensitivity * Time.deltaTime);
    }
}