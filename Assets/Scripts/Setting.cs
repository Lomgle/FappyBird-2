using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    /// //////////////////////
    
    private int lastWidth;
    private int lastHeight;
    private const float Ratio = 16.0f / 9.0f;

    /// //////////////////////
    
    public bool Fullscreen = true;

    /// //////////////////////
    
    public Toggle toggle;
    /// //////////////////////
    
    void Start()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;
        Application.targetFrameRate = 60;
        if (PlayerPrefs.HasKey("MUSIC"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MUSIC");
        }
        if (PlayerPrefs.HasKey("SFX"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("SFX");
        }
        if (PlayerPrefs.HasKey("FULLSCREEN"))
        {
            if (PlayerPrefs.GetInt("FULLSCREEN") == 1) {
                toggle.isOn = true;
                Fullscreen = true;
            }
            else {
                toggle.isOn = false;
                Fullscreen = false;
            }
        }
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("MUSIC", volume);
    }

    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("SFX", volume);
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        
        if (!isFullscreen) {
            Fullscreen = false;
            PlayerPrefs.SetInt("FULLSCREEN", 0);
        }
        else {
            Fullscreen = true;
            PlayerPrefs.SetInt("FULLSCREEN", 1);
        }
    }

    public void LockWindowRatio()
    {
        int width = Screen.width;
        int height = Screen.height;

        if (width != lastWidth)
        {
            int newHeight = Mathf.RoundToInt(width / Ratio);
            Screen.SetResolution(width, newHeight, false);
        }
        if (height != lastHeight)
        {
            int newWidth = Mathf.RoundToInt(height * Ratio);
            Screen.SetResolution(newWidth, height, false);
        }

        lastWidth = Screen.width;
        lastHeight = Screen.height;
    }

    public void FurinaSetting(bool isActive)
    {
        if (isActive) PlayerPrefs.SetInt("FURINA", 1);
        else PlayerPrefs.SetInt("FURINA", 0);
    }

    void Update()
    {
        if (!Fullscreen) LockWindowRatio();
    }
}
