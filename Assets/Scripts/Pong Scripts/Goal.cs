using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject gameManager;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (gameObject.name == "Score2")
            {
                gameManager.GetComponent<GameManager>().Player1Score();
            }
            else if (gameObject.name == "Score1")
            {
                gameManager.GetComponent<GameManager>().Player2Score();
            }
        }
    }
}
