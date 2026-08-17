using UnityEngine;

public class Lighting : MonoBehaviour
{
    public AudioSource thunder;

    public void PlayThunder()
    {
        thunder.Play();
    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 6f);
    }
}
