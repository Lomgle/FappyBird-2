using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> spriteList;

    public float moveSpeed = 5f;
    public float destroyZone = -15f;
    // Update is called once per frame

    void Start()
    {
        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Count)];
    }
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < destroyZone) Destroy(gameObject);
    }
}
