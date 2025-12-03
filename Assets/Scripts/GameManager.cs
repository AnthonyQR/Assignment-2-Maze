using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    // Player actions
    private PlayerActions _inputActions;
    private InputAction _reset;
    private InputAction _toggleDayNight;
    private InputAction _toggleFog;

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

        _reset.performed += ResetMaze;
        _toggleDayNight.performed += ToggleDayNight;
        _toggleFog.performed += ToggleFog;

        _reset.Enable();
        _toggleDayNight.Enable();
        _toggleFog.Enable();
    }

    private void OnDisable()
    {
        _reset.performed -= ResetMaze;
        _toggleDayNight.performed -= ToggleDayNight;
        _toggleFog.performed -= ToggleFog;

        _reset.Disable();
        _toggleDayNight.Disable();
        _toggleFog.Disable();
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

        // TODO: Update score in file
    }
}
