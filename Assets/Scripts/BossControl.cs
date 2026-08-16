using System.Collections;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public Animator skyAnim;
    public Animator bird;
    public Animator bossTitle;
    public Animator bossAnim;
    /// /////////////////////////
    public AudioSource backgroundMusic1;
    
    /// /////////////////////////
    public ParticleSystem rainParticle;
    /// /////////////////////////
    public GameObject lighting;
    public void beginSequence()
    {
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        backgroundMusic1.Play();
        skyAnim.SetTrigger("THUNDER");
        bird.SetTrigger("WET");

        bossAnim.SetTrigger("BOSS_IN");
        yield return new WaitForSeconds(5f);

        rainParticle.Play();

        yield return new WaitForSeconds(3.8f);

        Instantiate(lighting, new Vector3(0f, 0f, 0f), transform.rotation);

        yield return new WaitForSeconds(2f);

        bossTitle.SetTrigger("APPEAR");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
