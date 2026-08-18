using UnityEngine;

public class AudioAnimate : MonoBehaviour
{
    public AudioSource bgSong;
    public AudioSource TLST;
    public AudioSource vsSusie;
    public AudioSource MTG;
    public AudioSource flashbang;
    public void PlayBgSong()
    {
        bgSong.Play();
    }
    public void PlayTLST()
    {
        vsSusie.Stop();
        TLST.Play();
    }
    public void PlayvsSusie()
    {
        bgSong.Stop();
        vsSusie.Play();
    }
    public void PlayMTG()
    {
        MTG.Play();
    }

    public void StopTLST()
    {
        TLST.Stop();
    }
    public void PlayFlashbang()
    {
        flashbang.Play();
    }

    public void SlowDownBgSong()
    {
        bgSong.pitch = 0.5f;
    }
}
