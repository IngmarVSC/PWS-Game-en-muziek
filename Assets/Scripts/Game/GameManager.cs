using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void GameOver()
    {
        Debug.Log("GAME OVER");
        Time.timeScale = 0f;
    }
}