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
    
    private int code = 367;
    public int currentCode = 0;
    public bool inPanel = false;
    
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
        if (currentCode == code) Debug.Log("matched");
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
