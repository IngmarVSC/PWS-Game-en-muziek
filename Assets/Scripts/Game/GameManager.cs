using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject gameOverPanel;

    void Start()
    {
        // Game starts paused until the player presses START
        Time.timeScale = 0f;

        startMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void StartGame()
    {
        startMenuPanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}