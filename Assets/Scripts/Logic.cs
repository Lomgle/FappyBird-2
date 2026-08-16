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
    public Animator crossfadeAnim;
    public Canvas gamePauseCanvas;
    public int score = 0;
    public bool isPaused = false;
    public bool inPanel = false;
    
    //////////////////////////////////
    public Bird bird;   
    public BossControl bossControl;
    public GameObject furina;
    public GameObject cloudSpawner;
    public GameObject pipeSpawner;
    //////////////////////////////////
    public AudioSource backgroundSong;
    public AudioSource scoreUp;

    
    /// //////////////////////////////
    public ParticleSystem windParticle;
    void Start()
    {
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
        if (PlayerPrefs.HasKey("TALKED") && PlayerPrefs.HasKey("VISITEDSHRINE"))
        {
            cloudSpawner.SetActive(false);
            pipeSpawner.SetActive(false);
            bossControl.beginSequence();
        } else {
            backgroundSong.Play();
            windParticle.Play();
            Time.timeScale = 0.0f;
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
        StartCoroutine(LoadNextScene("Gameplay"));
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadNextScene("Menu"));
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

    IEnumerator LoadNextScene(string scene_name)
    {
        crossfadeAnim.SetTrigger("CROSSFADE");
        yield return new WaitForSecondsRealtime(0.25f);
        SceneManager.LoadScene(scene_name);
    }
}
