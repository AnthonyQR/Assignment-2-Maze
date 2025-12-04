using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bumper : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;

    private InputActions inputActions;
    private InputAction movement;
    private Vector2 moveInput;

    private void Awake()
    {
        inputActions = new InputActions();
        movement = inputActions.Player.Move;
    }

    private void OnEnable()
    {
        movement.Enable();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0f, moveInput.y * speed);
    }
}
