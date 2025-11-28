using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Get character controller to move
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private LayerMask _defaultLayer;
    [SerializeField] private LayerMask _exclusionLayer;
    [SerializeField] private Camera _playerCamera;
    [Space]

    // Movement variables
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _gravity = -0.4f;
    private float _fallingVelocity = 0f; 
    [Space]

    // Reference Audio Script
    [SerializeField] private PlayerAudio _playerAudioScript;

    // Player input
    private PlayerActions _inputActions;
    private InputAction _movement;
    private InputAction _godMode;
    private InputAction _changePerspective;

    private bool _godModeEnabled = false;

    // Get character controller & player inputs
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();
        _inputActions = new PlayerActions();
    }

    // Enable & disable input
    private void OnEnable()
    {
        _movement = _inputActions.Ingame.Movement;
        _godMode = _inputActions.Ingame.GodMode;
        _changePerspective = _inputActions.Ingame.ChangePerspective;

        _godMode.performed += ToggleGodMode;
        _changePerspective.performed += ToggleCamera; // Placeholder for perspective change
        _godMode.Enable();
        _movement.Enable();
        _changePerspective.Enable();
    }

    private void OnDisable()
    {
        _godMode.performed -= ToggleGodMode;
        _changePerspective.performed -= ToggleCamera;
        _movement.Disable();
        _godMode.Disable();
        _changePerspective.Disable();
    }


    // Always update to move player, regardless of Timescale
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        // Get input & put it into vector 3
        Vector2 v2 = _movement.ReadValue<Vector2>();
        HandleGroundMovement(v2);
    }

    private void HandleGroundMovement(Vector2 v2)
    {
        Vector3 velocity = new Vector3(v2.x, 0, v2.y);

        // Set x & z velocity based on player input & where they are facing
        velocity = transform.TransformDirection(velocity);
        velocity.x *= _movementSpeed * Time.unscaledDeltaTime;
        velocity.z *= _movementSpeed * Time.unscaledDeltaTime;

        
        if (_godModeEnabled)
        {
            // Don't fall.
        }

        // Check if player is on the ground.
        else if (_controller.isGrounded)
        {
            // Reset falling velocity & stick player to the ground.
            velocity.y = -0.05f;
            _fallingVelocity = 0f;
        }
        else
        {
            // Accumulate falling velocity & set the current velocity.
            _fallingVelocity += _gravity * Time.unscaledDeltaTime;

            velocity.y = _fallingVelocity;
        }

        _controller.Move(velocity);

        // Play walking sound if player is moving on the ground.
        if (_controller.isGrounded && (velocity.x != 0f || velocity.z != 0f))
        {
            _playerAudioScript.PlayWalkingSound();
        }
        else
        {
            _playerAudioScript.StopPlayingWalkingSound();
        }
    }

    private void ToggleGodMode(InputAction.CallbackContext callback)
    {
        if (!_godModeEnabled)
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionY
                | RigidbodyConstraints.FreezeRotation;
            _controller.excludeLayers = _exclusionLayer;
            _godModeEnabled = true;
            
        }
        else
        {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
            _controller.excludeLayers = _defaultLayer;
            _godModeEnabled = false;
        }
    }

    private void ToggleCamera(InputAction.CallbackContext callback)
    {
        _playerCamera.enabled = !_playerCamera.enabled;
    }

    public void ResetPosition()
    {
        _controller.enabled = false;
        transform.position = new Vector3(0, 0.25f, 0);
        _controller.enabled = true;
    }
}
