using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLogic : MonoBehaviour
{

    public Animator gameSetting;
    public Toggle furinaToggle;
    /// ///////////////////////////////////
    
    public bool inPanel = false;

    /// ///////////////////////////////////
    
    public GameObject secretLock;

    /// ///////////////////////////////////
    
    public ParticleSystem particleSystem;
    public Animator crossfadeAnim;

    void Start()
    {
        particleSystem.Play();
        if (!PlayerPrefs.HasKey("FURINA")) PlayerPrefs.SetInt("FURINA", 0);
    }
    public void LoadGame()
    {
       StartCoroutine(LoadNextScene("Gameplay"));
    }

    public void EnterShrine()
    {
        StartCoroutine(LoadNextScene("Cshrine"));
    }
    public void SettingGame()
    {
        gameSetting.SetTrigger("GAMESETTING");
        inPanel = true;
    }

    public void QuitSetting()
    {
        gameSetting.SetTrigger("OUTSETTING");
        inPanel = false;
    }
    public void QuitGame(){
        Application.Quit();
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame && inPanel)
        {
            QuitSetting();
        }
        
        if (PlayerPrefs.GetInt("FURINA") == 0) {
            furinaToggle.isOn = false;
            secretLock.SetActive(false);
        }
        else
        {
            furinaToggle.isOn = true;
            secretLock.SetActive(true);
        }
    }

    public IEnumerator LoadNextScene(string scene_name)
    {
        crossfadeAnim.SetTrigger("CROSSFADE");
        yield return new WaitForSecondsRealtime(0.25f);
        SceneManager.LoadScene(scene_name);
    }
}
