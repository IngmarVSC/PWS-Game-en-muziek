using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;
    public GameObject gameOverPanel;
    public GameObject gameControlsPanel;

    

    public float score;
    public float survivalTime;

    private bool gameRunning;
    private bool gameOver;

    void Start()
    {
        // game starts paused until the player presses START
        Time.timeScale = 0f;

        startMenuPanel.SetActive(true);
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameControlsPanel.SetActive(false);

        

        score = 0f;
        survivalTime = 0f;

        gameRunning = false; 
        gameOver = false;
    }

    public void Update()
    {
        if(!gameRunning || gameOver)
            return;

        survivalTime += Time.deltaTime;

        //score is just 1 point per sec
        //TODO: make score increase by near misses of asteroids (or collecting stuff / coins ? )
        score += Time.deltaTime;

    }


    public void StartGame()
    {   

        startMenuPanel.SetActive(false);
        
        gamePlayPanel.SetActive(true);

        gameRunning = true;
        gameOver = false;

        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;
        gameRunning = false;

        Debug.Log("GAME OVER");
        Debug.Log("Score: " + (Mathf.FloorToInt(score) -10)); // -10 because game doesnt actually start until after 10sec
        Debug.Log("Survival time: " + (survivalTime - 10));


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
