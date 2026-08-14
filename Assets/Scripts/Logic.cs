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
    public Animator gameSetting;
    public Canvas gamePauseCanvas;
    public int score = 0;
    public bool isPaused = false;
    public bool inPanel = false;
    
    //////////////////////////////////
    public Bird bird;   
    public GameObject furina;

    //////////////////////////////////
    public AudioSource backgroundSong;
    public AudioSource scoreUp;
    void Start()
    {
        backgroundSong.Play();
        bird = GameObject.FindGameObjectWithTag("Player").GetComponent<Bird>();
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

    }
    public void AddScore(int scoreToAdd = 1)
    {
        score += scoreToAdd;
        scoreUp.Play();
        scoreDisplay.text = score.ToString();
    }

    public IEnumerator GameOver()
    {
        Time.timeScale = 0.0f;
        backgroundSong.Pause();
        yield return new WaitForSecondsRealtime(1.2f);
        backgroundSong.UnPause();
        backgroundSong.volume = 0.02f;
        backgroundSong.pitch = 0.5f;

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
        SceneManager.LoadScene("Menu");
    }

    public void SettingGame()
    {
        inPanel = true;
        gameSetting.SetTrigger("GAMESETTING");
    }

    public void QuitSettingGame()
    {
        inPanel = false;
        gameSetting.SetTrigger("OUTSETTING");
    }

    public void PauseGame()
    {
        isPaused = true;
        backgroundSong.volume = 0.02f;
        Time.timeScale = 0.0f;
        gamePause.SetTrigger("GAMEPAUSE");
    }
    public void ResumeGame()
    {
        isPaused = false;
        backgroundSong.volume = 0.07f;
        gamePause.SetTrigger("GAMERESUME");
        Time.timeScale = 1.0f;
        Debug.Log("resumed");
    }
 
    void Update()
    {
        if (PlayerPrefs.GetInt("FURINA") == 1) furina.SetActive(true);
        else furina.SetActive(false);

        if (Keyboard.current.escapeKey.wasPressedThisFrame && bird.isAlive)
        {
            if (!isPaused) PauseGame();
            else if (!inPanel) ResumeGame();
            else QuitSettingGame();
        }

    }
}
