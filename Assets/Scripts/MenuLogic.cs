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
    
    public ParticleSystem particleSystem;

    void Start()
    {
        particleSystem.Play();
        if (!PlayerPrefs.HasKey("FURINA")) PlayerPrefs.SetInt("FURINA", 0);
        if (PlayerPrefs.GetInt("FURINA") == 0) furinaToggle.isOn = false;
        else furinaToggle.isOn = true;
    }
    public void LoadGame()
    {
       SceneManager.LoadScene("Gameplay");
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
    }
}
