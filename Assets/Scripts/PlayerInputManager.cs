using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [SerializeField] private GameObject bumperPrefab;
    [SerializeField] private ComputerBumper computerBumperPrefab;
    [SerializeField] private Transform[] spawnPoints;
    public GameObject outputText;
    public Ball ball;

    private HashSet<Gamepad> joinedGamepads = new HashSet<Gamepad>();
    private bool wsJoined = false;
    private bool upDownJoined = false;

    private int playersJoined = 0;
    private bool gamestarted = false;
    private int spawnIndex;

    public void Start()
    {
        spawnIndex = playersJoined;

        ComputerJoin();
    }

    public void ComputerJoin()
    {
        var computerBumper = Instantiate(computerBumperPrefab);
        computerBumper.SetBall(ball.GetComponent<Rigidbody2D>());
        computerBumper.transform.position = spawnPoints[spawnPoints.Length - 1].position;
        playersJoined++;
    }

    // Update is called once per frame
    void Update()
    {
        // Listen for join inputs while there is less than 2 players
        if (playersJoined < 2)
        {
            // Join with WS keys
            if (!wsJoined && Keyboard.current.wKey.wasPressedThisFrame)
            {
                var playerInput = PlayerInput.Instantiate(bumperPrefab, controlScheme: "WS", pairWithDevice: Keyboard.current);
                playerInput.transform.position = spawnPoints[spawnIndex].position;
                playersJoined++;
                spawnIndex++;
                wsJoined = true;
            }

            // Join with UpDown keys
            if (!upDownJoined && Keyboard.current.upArrowKey.wasPressedThisFrame)
            {
                var playerInput = PlayerInput.Instantiate(bumperPrefab, controlScheme: "UpDown", pairWithDevice: Keyboard.current);
                playerInput.transform.position = spawnPoints[spawnIndex].position;
                playersJoined++;
                spawnIndex++;
                upDownJoined = true;
            }

            // Join with gamepad
            foreach (var gamepad in Gamepad.all)
            {
                if (gamepad.dpad.up.wasPressedThisFrame && !joinedGamepads.Contains(gamepad))
                {
                    var playerInput = PlayerInput.Instantiate(bumperPrefab, controlScheme: "Gamepad", pairWithDevice: gamepad);
                    joinedGamepads.Add(gamepad);
                    playerInput.transform.position = spawnPoints[spawnIndex].position;
                    playersJoined++;
                    spawnIndex++;
                }
            }
        }

        // Start the game when two players have joined
        if (!gamestarted && playersJoined == 2)
        {
            outputText.GetComponent<TextMeshProUGUI>().text = "";
            ball.Reset();

            gamestarted = true;
        }
    }
}
