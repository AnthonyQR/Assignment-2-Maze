using UnityEngine;
using UnityEngine.SceneManagement;

public class PongDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        SceneManager.LoadScene("GameScene");
    }
}
