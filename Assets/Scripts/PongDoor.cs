using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PongDoor : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature _fogRendererFeature;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _fogRendererFeature.SetActive(false);
            SceneManager.LoadScene("GameScene");
        } 
    }
}
