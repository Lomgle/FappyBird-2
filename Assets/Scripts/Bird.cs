using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> spriteList;
    public float animateSpeed = 0.15f;
    private int spriteIndex;

    ////////////////
    
    public Rigidbody2D bird;
    public float flapStrength = 10f;
    public float mobileStrength = 2f;
    public bool isAlive = true;

    ////////////////
    
    public GameObject hintText;

    ////////////////
    
    public Logic logic;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        InvokeRepeating(nameof(AnimateSprite), animateSpeed, animateSpeed);
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= spriteList.Count)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = spriteList[spriteIndex];
    }

    void Update()
    {
        if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && isAlive && !logic.isPaused)
        {
            Time.timeScale = 1.0f;
            hintText.SetActive(false);
            bird.linearVelocityY = flapStrength;
        }
        if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) && PlayerPrefs.HasKey("TALKED"))
        {
            Time.timeScale = 1.0f;
            hintText.SetActive(false);
            bird.linearVelocityX = -mobileStrength;
        }
        if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) && PlayerPrefs.HasKey("TALKED"))
        {
            Time.timeScale = 1.0f;
            hintText.SetActive(false);
            bird.linearVelocityX = mobileStrength;
        }
        if ((transform.position.y > 7 || transform.position.y < -7) && isAlive)
        {
            isAlive = false;
            StartCoroutine(logic.GameOver());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isAlive = false;
        StartCoroutine(logic.GameOver());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreTrigger") && isAlive)
        {
            logic.AddScore();
        }
    }
}
