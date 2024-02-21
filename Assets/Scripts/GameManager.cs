using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public Text gameOverText;
    public Text winText;
    public Button restartButtonGameOver;
    public Button restartButtonWin;

    private bool isGameOver = false;
    private bool hasPlayerWon = false;

    private PlayerController playerController;

    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "GAME OVER";
        restartButtonGameOver.interactable = true;

        Time.timeScale = 0;
    }

    // Call this method to show the win screen
    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
        winText.text = "YOU WON";
        restartButtonWin.interactable = true;

        Time.timeScale = 0;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (!isGameOver)
        {
            CheckWinCondition();
            CheckLoseCondition();
        }
    }

    void CheckWinCondition()
    {
        if (playerController.HasReachedCrystalBall())
        {
            hasPlayerWon = true;
            isGameOver = true;
            ShowWinScreen();
        }
    }

    void CheckLoseCondition()
    {
        if (playerController.GetCurrentHealth() <= 0)
        {
            hasPlayerWon = false;
            isGameOver = true;
            ShowGameOverScreen();
        }
    }

    public void RestartGame()
    {
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        restartButtonGameOver.interactable = false;
        restartButtonWin.interactable = false;

        Time.timeScale = 1;

        SceneManager.LoadScene("MainGameplayScene");
    }

    public void ExitGame()
    {
        Application.Quit(); // This will close the game window
    }
}
