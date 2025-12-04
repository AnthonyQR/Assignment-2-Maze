using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PongDoor : MonoBehaviour
{
    [SerializeField] private FullScreenPassRendererFeature _fogRendererFeature;
    [SerializeField] private FullScreenPassRendererFeature _flashlightRendererFeature;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _fogRendererFeature.SetActive(false);
            _flashlightRendererFeature.SetActive(false);
            SceneManager.LoadScene("GameScene");
        } 
    }
}
