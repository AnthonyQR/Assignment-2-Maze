using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class MusicPlayer : MonoBehaviour
{
    // Music source reference.
    [SerializeField] private AudioSource _musicSource;
    [Space]

    // Day & Night themes.
    [SerializeField] private AudioClip _dayTheme;
    [SerializeField] private AudioClip _nightTheme;
    [Space]

    // Volume settings.
    [SerializeField][Range(0.0f, 1.0f)] private float _minimumVolume;
    [SerializeField][Range(0.0f, 1.0f)] private float _maximumVolume;

    // Fog bool.
    private bool _isFogEnabled = false;

    // Maximum distance threshold from player to enemy.
    // Distance needs to be lower than this float to start modulating volume.
    [SerializeField] private float _maximumModulatingDistance;

    // Player input.
    private PlayerActions _inputActions;
    private InputAction _toggleMusic;


    void Awake()
    {
        // Set up music player
        _inputActions = new PlayerActions();
        _isFogEnabled = false;
        _musicSource.clip = _dayTheme;
        _musicSource.volume = _minimumVolume;
    }

    private void OnEnable()
    {
        // Add toggle music input to function
        _toggleMusic = _inputActions.Ingame.ToggleMusic;
        _toggleMusic.performed += ToggleMusic;
        _toggleMusic.Enable();
        
    }

    private void OnDisable()
    {
        // Remove toggle music input from function
        _toggleMusic.performed -= ToggleMusic;
        _toggleMusic.Disable(); 
    }

    private void ToggleMusic(InputAction.CallbackContext context)
    {
        // Toggle music on / off.
        if (_musicSource.isPlaying)
        {
            _musicSource.Stop();
        }
        else
        {
            _musicSource.Play();
        }
    }
    public void ToggleDayNight()
    {
        // Swap between day & night themes
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

    public void ToggleFog()
    {
        // Enable / disable fog.
        // Volume changes in ModulateVolume depending on this bool.
        if (_isFogEnabled)
        {
            _isFogEnabled = false;
        }
        else
        {
            _isFogEnabled = true;
        }
    }

    public void ModulateVolume(GameObject player, GameObject enemy)
    {
        // Get player & enemy positions
        Vector3 PlayerPosition = player.transform.position;
        Vector3 EnemyPosition = enemy.transform.position;

        // Get x & z differences for player & enemy
        float EnemyToPlayerX = EnemyPosition.x - PlayerPosition.x;
        float EnemyToPlayerZ = EnemyPosition.z - PlayerPosition.z;

        // Calculate total distance from player & enemy with pythagoras.
        float EnemyToPlayer = Mathf.Sqrt(Mathf.Pow(EnemyToPlayerX, 2) 
            + Mathf.Pow(EnemyToPlayerZ, 2));

        // Prepare new music float for calculations
        float newMusicVolume = _minimumVolume;

        // Change volume if player is close enough to the enemy.
        if (EnemyToPlayer < _maximumModulatingDistance)
        {
            newMusicVolume = (_maximumModulatingDistance - EnemyToPlayer) 
                / _maximumModulatingDistance * _maximumVolume;

            // Make sure volume is above the minimum.
            if (newMusicVolume < _minimumVolume)
            {
                newMusicVolume = _minimumVolume;
            }
        }

        // Half volume if fog is enabled.
        if (_isFogEnabled)
        {
            newMusicVolume /= 2;
        }

        // Change volume of the music source.
        _musicSource.volume = newMusicVolume;
    }
}
