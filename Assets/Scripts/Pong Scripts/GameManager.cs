using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject score1;
    [SerializeField] private GameObject score2;
    [SerializeField] private GameObject scoreText1;
    [SerializeField] private GameObject scoreText2;
    [SerializeField] private GameObject WinnerDisplay;
    [SerializeField] private GameObject MainMenuButton;

    [SerializeField] private int winScore = 11;
    private int scoreInt1;
    private int scoreInt2;

    public void Player1Score()
    {
        scoreInt1++;
        scoreText1.GetComponent<TextMeshProUGUI>().text = scoreInt1.ToString();
        ResetBall();
    }

    public void Player2Score()
    {
        scoreInt2++;
        scoreText2.GetComponent<TextMeshProUGUI>().text = scoreInt2.ToString();
        ResetBall();
    }

    private void ResetBall()
    {
        CheckWin();
        ball.GetComponent<Ball>().Reset();
    }

    private void CheckWin()
    {
        if (scoreInt1 >= winScore)
        {
            WinnerDisplay.GetComponent<TextMeshProUGUI>().text = "Player 1 Wins!";
            ball.SetActive(false);
            MainMenuButton.SetActive(true);
        }
        else if (scoreInt2 >= winScore)
        {
            WinnerDisplay.GetComponent<TextMeshProUGUI>().text = "Player 2 Wins!";
            ball.SetActive(false);
            MainMenuButton.SetActive(true);
        }
    }
}
