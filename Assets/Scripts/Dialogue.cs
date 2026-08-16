using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{

    public List<string> dialogues;
    public int message_index;
    public TextMeshProUGUI message;
    public bool isTyping = false;
    public float typeSpeed;
    void Start()
    {
        message_index = 0;
        message.text = dialogues[message_index];
    }

    public void NextConvo()
    {
        message.text = String.Empty;
        message_index++;
        if (message_index < dialogues.Count)
        {
            StartCoroutine(DialogueAnimate(message_index,  typeSpeed));
        } else gameObject.SetActive(false);
    }

    public void SkipConvo()
    {
        StopAllCoroutines();
        message.text = dialogues[message_index];
        isTyping = false;
    }

    IEnumerator DialogueAnimate(int index, float typeSpeed = 2f)
    {
        isTyping = true;
        foreach (char c in dialogues[index].ToCharArray())
        {
            message.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (isTyping) SkipConvo();
            else NextConvo();
        }
    }
}
