using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicPlayer : MonoBehaviour
{
    // Music source reference
    [SerializeField] private AudioSource _musicSource;
    [Space]

    // Day & Night themes
    [SerializeField] private AudioClip _dayTheme;
    [SerializeField] private AudioClip _nightTheme;
    [Space]

    // Fog volume
    [SerializeField][Range(0.0f, 1.0f)] private float _noFogVolume;
    [SerializeField][Range(0.0f, 1.0f)] private float _fogVolume;
    private bool _isFogEnabled = false;

    // Player input
    private PlayerActions _inputActions;
    private InputAction _toggleMusic;
    private InputAction _toggleDayNight;
    private InputAction _toggleFog;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Log("Music");
        _inputActions = new PlayerActions();
        _isFogEnabled = false;
        _musicSource.clip = _dayTheme;
        _musicSource.volume = _noFogVolume;
    }

    private void OnEnable()
    {
        _toggleMusic = _inputActions.Ingame.ToggleMusic;
        _toggleDayNight = _inputActions.Ingame.ToggleDayNight;
        _toggleFog = _inputActions.Ingame.ToggleFog;

        _toggleMusic.performed += ToggleMusic;
        _toggleDayNight.performed += ToggleDayNight;
        _toggleFog.performed += ToggleFog;

        _toggleMusic.Enable();
        _toggleDayNight.Enable();
        _toggleFog.Enable();
    }

    private void OnDisable()
    {
        _toggleMusic.performed -= ToggleMusic;
        _toggleDayNight.performed -= ToggleDayNight;
        _toggleFog.performed -= ToggleFog;

        _toggleMusic.Disable();
        _toggleDayNight.Disable();
        _toggleFog.Disable();
    }

    private void ToggleMusic(InputAction.CallbackContext context)
    {
        Debug.Log("Music");
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
        else
        {
            _musicSource.Play();
        }
    }
    private void ToggleDayNight(InputAction.CallbackContext context)
    {
        _musicSource.Stop();

        if (_musicSource.clip == _dayTheme)
        {
            _musicSource.clip = _nightTheme;
        }
        else
        {
            _musicSource.clip = _dayTheme;
        }

        _musicSource.Play();
    }

    private void ToggleFog(InputAction.CallbackContext context)
    {
        if (_isFogEnabled)
        {
            _isFogEnabled = false;
            _musicSource.volume = _noFogVolume;
        }
        else
        {
            _isFogEnabled = true;
            _musicSource.volume = _fogVolume;
        }
    }
}
