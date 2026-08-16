using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThangShrine : MonoBehaviour
{
    public GameObject fakeButton;
    public GameObject dialogueObject;
    public GameObject backButton;
    public GameObject miku;
    public GameObject miku_dialogue;
    public Dialogue dialogue;
    public Animator thangAnim;
    public Animator crossfadeAnim;
    /// //////////////////////////
    
    public SpriteRenderer spriteRenderer;
    public Sprite normal;
    void Start()
    {
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();
        if (PlayerPrefs.HasKey("VISITEDSHRINE"))
        {
            PlayerPrefs.SetInt("TALKED", 1);
            spriteRenderer.color = Color.clear;
            ThangLeft();
            miku.SetActive(true);
            miku_dialogue.SetActive(true);
            AudioListener.volume = .05f;
        }
    }

    public void ThangLeft()
    {
        dialogueObject.SetActive(false);
        thangAnim.SetTrigger("GETOUT");
        backButton.SetActive(true);
    }

    public void QuitShrine()
    {
        StartCoroutine(LoadNextScene("Menu"));
    }
    void Update()
    {
        if (dialogue.message_index == 3 
        || dialogue.message_index == 9
        || dialogue.message_index == 11
        || dialogue.message_index == 14
        || dialogue.message_index == 26) fakeButton.SetActive(true);
        else fakeButton.SetActive(false);

        if (dialogue.message_index == 41)
        {
            ThangLeft();
            PlayerPrefs.SetInt("VISITEDSHRINE", 1);
        }

        if (dialogue.message_index == 15)
        {
            spriteRenderer.color = Color.softRed;
        }
        if (dialogue.message_index == 4) {
            spriteRenderer.sprite = normal;
            transform.localScale = new Vector3(1.26f, 0.32f);
        }
    }

    IEnumerator LoadNextScene(string scene_name)
    {
        crossfadeAnim.SetTrigger("CROSSFADE");
        yield return new WaitForSecondsRealtime(0.25f);
        AudioListener.volume = 1;
        SceneManager.LoadScene(scene_name);
    }
}
