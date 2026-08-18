using System.Collections;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public Animator skyAnim;
    public Animator bird;
    public Animator bossTitle;
    public Animator bossAnim;
    public Animator finalCutscene;
    public Animator cameraAnim;
    /// /////////////////////////
    public AudioSource backgroundMusic1;
    
    /// /////////////////////////
    public ParticleSystem rainParticle;
    public ParticleSystem biggerRainParticle;
    /// /////////////////////////
    public GameObject fogTint;
    public GameObject lighting;
    public GameObject pipeSpawn;
    public GameObject pipeSpawnBackward;
    public GameObject pipeSpawnDiagonal;
    public GameObject pipeLunge;
    public GameObject pipeDangerous;
    public GameObject pipeLungeSlow;

    public GameObject pipeSpawnerNormal;
    public GameObject cloudSpawner;
    /// ///////////////////////////////
    public Logic logic;
    public Bird birdObject;
    public void beginSequence()
    {
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        cameraAnim.SetTrigger("WIGGLE");
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
        logic.SetScore(36);
        StartCoroutine(StageOne());
    }

    IEnumerator StageOne()
    {
        for (int i = 1; i <= 5; i++)
        {
            Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation);
            yield return new WaitForSeconds(2f);
        }
        pipeLunge.SetActive(true);
        for (int i = 1; i <= 5; i++)
        {
            Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation);
            yield return new WaitForSeconds(1.5f);
        }

        pipeLunge.SetActive(false);
        StartCoroutine(StageTwo());
    }

    IEnumerator StageTwo()
    {
        pipeDangerous.SetActive(true);
        yield return new WaitForSeconds(4.5f);
        pipeLungeSlow.SetActive(true);
        yield return new WaitForSeconds(4.5f);

        pipeLungeSlow.SetActive(false);
        for (int i = 1; i <= 7; i++)
        {
            Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation);
            yield return new WaitForSeconds(2.0f);
        }
        pipeDangerous.SetActive(false);

        for (int i = 1; i <= 3; i++) Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation);
        pipeLunge.SetActive(true);
        yield return new WaitForSeconds(2f);
        pipeLunge.SetActive(false);
        StartCoroutine(StageThree());
    }

    IEnumerator StageThree()
    {
        pipeSpawn.SetActive(true);
        yield return new WaitForSeconds(1f);
        pipeSpawnBackward.SetActive(true);

        yield return new WaitForSeconds(1f);
        for (int i = 1; i <= 5; i++)
        {
            Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation);  
            yield return new WaitForSeconds(2f);
        }
        pipeSpawn.SetActive(false);
        pipeSpawnBackward.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpamLaser(10));
        rainParticle.Stop();

        yield return new WaitForSeconds(9f);
        pipeSpawnDiagonal.SetActive(true);

        yield return new WaitForSeconds(1f);
        biggerRainParticle.Play();
        fogTint.SetActive(true);

        yield return new WaitForSeconds(9f);
        biggerRainParticle.Stop();
        fogTint.SetActive(false);
        pipeSpawnDiagonal.SetActive(false);
        skyAnim.SetTrigger("SUNNY");
        bird.SetTrigger("DRY");

        yield return new WaitForSeconds(1.5f);
        backgroundMusic1.Stop();
        cameraAnim.SetTrigger("NORMAL");

        yield return new WaitForSeconds(1f);
        bossAnim.SetTrigger("FINAL");
        yield return new WaitForSeconds(5f);
        birdObject.freezeBird = true;
        StartCoroutine(StageFinal());
    }

    IEnumerator StageFinal()
    {
        finalCutscene.SetTrigger("PLAY");
        yield return new WaitForSeconds(92f);
        pipeSpawnerNormal.SetActive(true);
        logic.AddScore(31);
        cloudSpawner.SetActive(true);
        birdObject.freezeBird = false;
    }

    IEnumerator SpamLaser(int time)
    {
        for (int i = 1; i <= time; i++)
        {
            if (i > time / 2)
            {
                Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation); 
            } else
            {
                Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation); 
                Instantiate(lighting, new Vector3(Random.Range(-6f, 6f), 0f, 0f), transform.rotation); 
            } 
            yield return new WaitForSeconds(2f);
        }
    }
}
