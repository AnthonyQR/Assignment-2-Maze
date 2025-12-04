using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class ShaderController : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature _fogRendererFeature;
    [SerializeField] private GameObject _flashLight;
    [SerializeField] private FullScreenPassRendererFeature _dayNightRendererFeature;

    private PlayerActions _inputActions;
    private InputAction _toggleFogAction;
    private InputAction _toggleFlashlightAction;
    private InputAction _toggleDayNightAction;

    private bool _isNight = false;
    private float _dayNight = 1f;

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
        _flashLight.SetActive(!_flashLight.activeInHierarchy);
    }

    private void OnToggleDayNight(InputAction.CallbackContext ctx)
    {
        _isNight = !_isNight;
        _dayNight = _isNight ? 0.75f : 0f;
        Debug.Log("DayNight toggled. IsNight: " + _isNight + ", DayNight value: " + _dayNight);
        Shader.SetGlobalFloat("_DayNight", _dayNight);
    }
}
