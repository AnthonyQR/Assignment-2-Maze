using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class MazeGameManager : MonoBehaviour
{
    // Player actions
    private PlayerActions _inputActions;
    private InputAction _reset;
    private InputAction _toggleDayNight;
    private InputAction _toggleFog;
    private InputAction _save;
    private InputAction _load;

    // Reference Prefabs
    public GameObject Player { get; set; }
    public GameObject Enemy { get; set; }

    // Reference Music Player Script
    public MusicPlayer MusicPlayerScript;

    // Reference Score
    public TextMeshProUGUI _scoreText;
    private int _score;

    private void Awake()
    {
        _inputActions = new PlayerActions();
    }

    private void Start()
    {
        // Set score text.
        _score = 0;
        _scoreText.text = string.Format("Score: {0}", _score);
    }

    private void OnEnable()
    {
        _reset = _inputActions.Ingame.Reset;
        _toggleDayNight = _inputActions.Ingame.ToggleDayNight;
        _toggleFog = _inputActions.Ingame.ToggleFog;
        _save = _inputActions.Ingame.Save;
        _load = _inputActions.Ingame.Load;

        _reset.performed += ResetMaze;
        _toggleDayNight.performed += ToggleDayNight;
        _toggleFog.performed += ToggleFog;
        _save.performed += SaveGame;
        _load.performed += LoadGame;

        _reset.Enable();
        _toggleDayNight.Enable();
        _toggleFog.Enable();
        _save.Enable();
        _load.Enable();
    }

    private void OnDisable()
    {
        _reset.performed -= ResetMaze;
        _toggleDayNight.performed -= ToggleDayNight;
        _toggleFog.performed -= ToggleFog;
        _save.performed -= SaveGame;
        _load.performed -= LoadGame;

        _reset.Disable();
        _toggleDayNight.Disable();
        _toggleFog.Disable();
        _save.Disable();
        _load.Disable();
    }

    private void Update()
    {
        // Modulate music on update.
        MusicPlayerScript.ModulateVolume(Player, Enemy);
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

    private void ToggleDayNight(InputAction.CallbackContext context)
    {
        MusicPlayerScript.ToggleDayNight();
    }

    private void ToggleFog(InputAction.CallbackContext context)
    {
        MusicPlayerScript.ToggleFog();
    }

    public void HitEnemy()
    {
        // Update score.
        _score += 1;
        _scoreText.text = string.Format("Score: {0}", _score);
    }

    private void SaveGame(InputAction.CallbackContext context)
    {
        // Save score.
        PlayerPrefs.SetInt("playerScore", _score);

        // Save player and enemy positions.
        PlayerPrefs.SetFloat("playerPosX", Player.transform.position.x);
        PlayerPrefs.SetFloat("playerPosY", Player.transform.position.y);
        PlayerPrefs.SetFloat("playerPosZ", Player.transform.position.z);

        PlayerPrefs.SetFloat("enemyPosX", Enemy.transform.position.x);
        PlayerPrefs.SetFloat("enemyPosY", Enemy.transform.position.y);
        PlayerPrefs.SetFloat("enemyPosZ", Enemy.transform.position.z);

        // Persist data to disk.
        PlayerPrefs.Save();
    }

    private void LoadGame(InputAction.CallbackContext context)
    {
        // Load score.
        _score = PlayerPrefs.GetInt("playerScore", 0);
        _scoreText.text = string.Format("Score: {0}", _score);

        // Load player and enemy positions.
        Vector3 loadedPlayerPosition = new Vector3(
            PlayerPrefs.GetFloat("playerPosX", 0f),
            PlayerPrefs.GetFloat("playerPosY", 0f),
            PlayerPrefs.GetFloat("playerPosZ", 0f)
        );

        Vector3 loadedEnemyPosition = new Vector3(
            PlayerPrefs.GetFloat("enemyPosX", 0f),
            PlayerPrefs.GetFloat("enemyPosY", 0f),
            PlayerPrefs.GetFloat("enemyPosZ", 0f)
        );
        CharacterController playerCharacterController = Player.GetComponent<CharacterController>();
        playerCharacterController.enabled = false;
        Player.transform.position = loadedPlayerPosition;
        playerCharacterController.enabled = true;

        Enemy.GetComponent<NavMeshAgent>().Warp(loadedEnemyPosition);
    }
}
