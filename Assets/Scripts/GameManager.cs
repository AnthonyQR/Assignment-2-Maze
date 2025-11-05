using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private PlayerActions _inputActions;
    private InputAction _reset;

    public GameObject Player { get; set; }
    public GameObject Enemy { get; set; }

    private void Awake()
    {
        _inputActions = new PlayerActions();
    }

    private void OnEnable()
    {
        _reset = _inputActions.Ingame.Reset;
        _reset.performed += ResetMaze;
        _reset.Enable();
    }

    private void OnDisable()
    {
        _reset.performed -= ResetMaze;
        _reset.Disable();
    }

    private void ResetMaze(InputAction.CallbackContext context)
    {
        // Reset player and enemy positions.
        Player.GetComponent<PlayerMovement>().ResetPosition();
        Enemy.GetComponent<Enemy>().ResetPosition();

        // Reset player camera back to default first-person view if it was set to top-down.
        Camera playerCamera = Player.GetComponentInChildren<Camera>();
        if (!playerCamera.enabled)
        {
            playerCamera.enabled = true;
        }
    }
}
