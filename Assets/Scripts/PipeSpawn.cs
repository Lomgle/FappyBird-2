using UnityEngine;

public class PipeSpawn : MonoBehaviour
{
    public GameObject obj;
    public float spawnInterval = 5f;
    public float heightVariance = 3f;
    private float time = 0f;
    private Vector3 newPos;
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= spawnInterval)
        {
            newPos.x = transform.position.x;
            newPos.y = transform.position.y + Random.Range(-heightVariance, heightVariance);
            Instantiate(obj, newPos, transform.rotation);

            time = 0;
        }
    }
}
