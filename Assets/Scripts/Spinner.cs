using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Vector3 spinDirection = new Vector3(0, 1, 0);
    public float spinSpeed = 50.0f;

    void Update()
    {
        transform.Rotate(spinDirection * (spinSpeed * Time.deltaTime));
    }
}
