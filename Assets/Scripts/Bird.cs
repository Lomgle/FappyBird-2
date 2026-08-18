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
    public bool freezeBird = false;

    ////////////////
    
    public GameObject hintText;
    private Vector3 vec3;

    ////////////////
    
    public Logic logic;
    void Start()
    {
        vec3.x = 0.0f;
        vec3.y = 0.0f;
        vec3.z = 0.0f;
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
        if (freezeBird)
        {
            transform.position = vec3;
            bird.linearVelocityY = 0.0f;
        } else {
            if ((Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && isAlive && !logic.isPaused)
            {
                Time.timeScale = 1.0f;
                hintText.SetActive(false);
                bird.linearVelocityY = flapStrength;
            }
            if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) && PlayerPrefs.HasKey("TALKED") && isAlive && !logic.isPaused)
            {
                Time.timeScale = 1.0f;
                hintText.SetActive(false);
                bird.linearVelocityX = -mobileStrength;
            }
            if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) && PlayerPrefs.HasKey("TALKED") && isAlive && !logic.isPaused)
            {
                Time.timeScale = 1.0f;
                hintText.SetActive(false);
                bird.linearVelocityX = mobileStrength;
            }
            if ((transform.position.y > 7 || transform.position.y < -7 || transform.position.x < -12 || transform.position.x > 12) && isAlive)
            {
                isAlive = false;
                StartCoroutine(logic.GameOver());
            }
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
