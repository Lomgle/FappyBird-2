using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Password : MonoBehaviour
{
    public Animator passwordPanel;
    /// //////////////////////////
    public TextMeshProUGUI password_display;
    /// //////////////////////////
    public MenuLogic logic;
    public int currentCode = 0;
    public bool inPanel = false;
    
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<MenuLogic>();
    }
    public void AddNum(int num)
    {
        if (currentCode % 10 > 0)
        {
            currentCode = currentCode - currentCode % 10;
            currentCode += num;
        }
        else if (currentCode == 0) currentCode += num * 100;
        else if (currentCode % 100 == 0) currentCode += num * 10;
        else currentCode += num;
    }

    public void ClearNum()
    {
        currentCode = 0;
    }

    public void CheckPassword()
    {
        if (currentCode == 367 || currentCode == 369){
            StartCoroutine(logic.LoadNextScene("Cshrine"));
        }
    }

    public void OpenPanel()
    {
        inPanel = true;
        passwordPanel.SetTrigger("OPEN");
    }

    public void QuitPanel()
    {
        inPanel = false;
        passwordPanel.SetTrigger("CLOSE");
    }

    void Update()
    {
        if (currentCode == 0) password_display.text = "000";
        else password_display.text = currentCode.ToString();

        if (Keyboard.current.escapeKey.wasPressedThisFrame && inPanel) QuitPanel();
    }
}
