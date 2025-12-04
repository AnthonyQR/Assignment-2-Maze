using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void LoadGameMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
}
