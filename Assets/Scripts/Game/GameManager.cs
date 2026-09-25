using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenuPanel;
    public GameObject gameOverPanel;
    public GameObject gameControlsPanel;

    void Start()
    {
        // game starts paused until the player presses START
        Time.timeScale = 0f;

        startMenuPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameControlsPanel.SetActive(false);
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

    // restarting game just resets the gamescene
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    public void ShowControls()
    {
        Time.timeScale = 0f;

        startMenuPanel.SetActive(false);
        gameControlsPanel.SetActive(true);
    }
}
