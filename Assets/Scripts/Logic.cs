using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;
    public Animator gameOver;
    public int score = 0;
    
    public void AddScore(int scoreToAdd = 1)
    {
        score += scoreToAdd;
        scoreDisplay.text = score.ToString();
    }

    public void GameOver()
    {
        gameOver.SetTrigger("GAMEOVER");
    }
    public void RetryGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Gameplay");
    }

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    
    void Update()
    {
        
    }
}
