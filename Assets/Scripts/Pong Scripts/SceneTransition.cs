using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public bool isSinglePlayer = false;

    public void LoadGameSceneSingleplayer()
    {
        isSinglePlayer = true;
        LoadGameScene();
    }

    public void LoadGameSceneMultiplayer()
    {
        LoadGameScene();
    }

    private void LoadGameScene()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("GameScene"); 
    }
}
