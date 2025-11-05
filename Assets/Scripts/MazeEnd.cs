using UnityEngine;

public class MazeEnd : MonoBehaviour
{
    [SerializeField] private GameObject _mazeEndCanvas;

    private void Start()
    {
       _mazeEndCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().enabled = false;
            _mazeEndCanvas.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
}
