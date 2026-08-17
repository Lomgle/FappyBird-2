using UnityEngine;

public class AdvancedPipeMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float destroyZone = -15f;

    public float fluctuateSpeed = 2.0f;
    public float fluctuateRate = 0.2f;

    private Vector3 originalPos;
    
    void Start()
    {
        originalPos = transform.position;
    }
    void Update()
    {
        float newPos = Mathf.Sin(fluctuateSpeed * Time.time) * fluctuateRate;

        transform.position += Vector3.up * newPos * Time.deltaTime;
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < destroyZone) Destroy(gameObject);
    }
}
