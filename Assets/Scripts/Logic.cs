using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI gameOverScoreDisplay;
    public TextMeshProUGUI gameOverBestScoreDisplay;

    //////////////////////////////////
    public Animator gameOver;
    public Animator gamePause;
    public Canvas gamePauseCanvas;
    public int score = 0;
    public bool isPaused = false;
    
    //////////////////////////////////
    public Bird bird;   

    void Start()
    {
        Application.targetFrameRate = 60;
        bird = GameObject.FindGameObjectWithTag("Player").GetComponent<Bird>();
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

    }
    public void AddScore(int scoreToAdd = 1)
    {
        score += scoreToAdd;
        scoreDisplay.text = score.ToString();
    }

    public IEnumerator GameOver()
    {
        Time.timeScale = 0.0f;
        yield return new WaitForSecondsRealtime(1.2f);
        gameOverScoreDisplay.text = score.ToString();
        if (score > PlayerPrefs.GetInt("BestScore")) PlayerPrefs.SetInt("BestScore", score);
        gameOverBestScoreDisplay.text = PlayerPrefs.GetInt("BestScore").ToString();
        gameOver.SetTrigger("GAMEOVER");
    }
    public void RetryGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Gameplay");
    }

    public void QuitGame()
    {
        ;
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0.0f;
        gamePause.SetTrigger("GAMEPAUSE");
    }
    public void ResumeGame()
    {
        isPaused = false;
        gamePause.SetTrigger("GAMERESUME");
        Time.timeScale = 1.0f;
        Debug.Log("resumed");
    }
 
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && bird.isAlive)
        {
            if (!isPaused) PauseGame();
            else ResumeGame();
        }

    }
}
