using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject gamePlayPanel;
    public GameObject startMenuPanel;
    public GameObject gameOverPanel;
    public GameObject gameControlsPanel;
    public GameObject player;
    
    public TextMeshProUGUI musicTypeText1;
    

    public RockSpawner rockSpawner;
    public AudioManager audioManager;

    public float score;
    public float survivalTime;

    private bool gameRunning;
    private bool gameOver; 

    void Start()
    {
        // game starts paused until the player presses START
        Time.timeScale = 0f;

        startMenuPanel.SetActive(true);
        player.SetActive(false);

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
        player.SetActive(true);

        gameRunning = true;
        gameOver = false;

        Time.timeScale = 1f;

        rockSpawner.StartSpawning();
        audioManager.PlayGameplayMusic();

        Debug.Log("CURRENT MUSIC TYPE: " + audioManager.currentMusicType);
    }

    public void GameOver()
    {
        if (gameOver)
            return;

        gameOver = true;
        gameRunning = false;

        Debug.Log("GAME OVER");
        Debug.Log("Score: " + Mathf.FloorToInt(score)); 

        Debug.Log("Survival time: " + survivalTime);

        musicTypeText1.text =
            "Music type: " + audioManager.currentMusicType;

        gameOverPanel.SetActive(true);



        audioManager.StopMusic();

        Time.timeScale = 0f;
    }

    public void CloseControls()
    {
        gameControlsPanel.SetActive(false);
        startMenuPanel.SetActive(true);
        player.SetActive(false);
    }

    public void ShowControls()
    {
        Time.timeScale = 0f;

        startMenuPanel.SetActive(false);
        gameControlsPanel.SetActive(true);

        // hide player during controls screen
        player.SetActive(false);
    }

    // restarting game just resets the gamescene
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
