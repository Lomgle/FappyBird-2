using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> spriteList;
    public float animateSpeed = 0.15f;
    private int spriteIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
