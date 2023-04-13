using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    private AudioSource speaker;
    public AudioClip[] clipArray;

    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();

        if (speaker.clip != null)
            speaker.clip = null;
    }

    public void bossMusicStart()
    {
        speaker.clip = clipArray[0];
        speaker.Play();
    }

    public void bossMusicEnd()
    {
        speaker.clip = null;
        speaker.Stop();

        speaker.PlayOneShot(clipArray[1]);
    }

    public void died()
    {
        speaker.clip = null;
        speaker.Stop();

        speaker.PlayOneShot(clipArray[2]);
    }

    public void swing()
    {
        speaker.PlayOneShot(clipArray[3]); /// 3 for boss swing 
    }
}
