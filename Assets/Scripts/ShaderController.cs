using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class ShaderController : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature _fogRendererFeature;
    [SerializeField] private FullScreenPassRendererFeature _flashlightRendererFeature;
    [SerializeField] private FullScreenPassRendererFeature _dayNightRendererFeature;

    private PlayerActions _inputActions;
    private InputAction _toggleFogAction;
    private InputAction _toggleFlashlightAction;
    private InputAction _toggleDayNightAction;

    private void Awake()
    {
        _inputActions = new PlayerActions();
    }

    private void OnEnable()
    {
        _toggleFogAction = _inputActions.Ingame.ToggleFog;
        _toggleFlashlightAction = _inputActions.Ingame.ToggleFlashlight;
        _toggleDayNightAction = _inputActions.Ingame.ToggleDayNight;

        _toggleFogAction.performed += OnToggleFogPerformed;
        _toggleFlashlightAction.performed += OnToggleFlashlight;
        _toggleDayNightAction.performed += OnToggleDayNight;

        _toggleFogAction.Enable();
        _toggleFlashlightAction.Enable();
        _toggleDayNightAction.Enable();
    }

    private void OnDisable()
    {
        _toggleFogAction.performed -= OnToggleFogPerformed;
        _toggleFlashlightAction.performed -= OnToggleFlashlight;
        _toggleDayNightAction.performed -= OnToggleDayNight;

        _toggleFogAction.Disable();
        _toggleFlashlightAction.Disable();
        _toggleDayNightAction.Disable();
    }

    private void OnToggleFogPerformed(InputAction.CallbackContext ctx)
    {
        _fogRendererFeature.SetActive(!_fogRendererFeature.isActive);
    }

    private void OnToggleFlashlight(InputAction.CallbackContext ctx)
    {
        _flashlightRendererFeature.SetActive(!_flashlightRendererFeature.isActive);
    }

    private void OnToggleDayNight(InputAction.CallbackContext ctx)
    {
        _dayNightRendererFeature.SetActive(!_dayNightRendererFeature.isActive);
    }
}
