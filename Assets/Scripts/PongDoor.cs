using UnityEngine;
using UnityEngine.SceneManagement;

public class PongDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameScene");
        } 
    }
}
