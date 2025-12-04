using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    [SerializeField] private TMP_InputField consoleInputField;
    [SerializeField] private GameObject consolePanel;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject ballPrefab;

    void Start()
    {
        consoleInputField.onEndEdit.AddListener(ProcessCommand);
    }

    void Update()
    {
        bool isTyping = consoleInputField.isFocused && Input.anyKeyDown;
        if (!isTyping && Input.GetKeyDown(KeyCode.C))
        {
            consolePanel.SetActive(!consolePanel.activeSelf);
            if (consolePanel.activeSelf)
            {
                consoleInputField.ActivateInputField();
            }
        }
    }

    void ProcessCommand(string command)
    {
        if (string.IsNullOrEmpty(command)) return;

        if (command.ToLower() == "bg blue")
        {
            mainCamera.GetComponent<Camera>().backgroundColor = Color.blue;
        }
        else if (command.ToLower() == "bg black")
        {
            mainCamera.GetComponent<Camera>().backgroundColor = Color.black;
        }
        else if (command.ToLower() == "main menu")
        {
            SceneManager.LoadScene("MainMenu");
        }

        consoleInputField.text = "";
        consoleInputField.ActivateInputField();
    }
}
