using System.Collections.Generic;
using System.Linq;
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
    public bool isAlive = true;

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
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isAlive)
        {
            bird.linearVelocity = Vector2.up * flapStrength;
        }
        if ((transform.position.y > 7 || transform.position.y < -7) && isAlive)
        {
            isAlive = false;
            logic.GameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isAlive = false;
        logic.GameOver();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScoreTrigger") && isAlive)
        {
            logic.AddScore();
        }
    }
}
