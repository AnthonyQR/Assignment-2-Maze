using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerThrowBall : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private Transform _ballGroup;
    [SerializeField] private Transform _ballSpawnPoint;

    private PlayerActions _inputActions;
    private InputAction _throwBall;

    private void Awake()
    {
        _inputActions = new PlayerActions();
    }

    private void OnEnable()
    {
        _throwBall = _inputActions.Ingame.ThrowBall;
        _throwBall.performed += ThrowBall;
        _throwBall.Enable();
    }

    private void OnDisable()
    {
        _throwBall.performed -= ThrowBall;
        _throwBall.Disable();
    }

    private void ThrowBall(InputAction.CallbackContext callback)
    {
        Instantiate(_ballPrefab, _ballSpawnPoint.position, _ballGroup.rotation);
    }
}
