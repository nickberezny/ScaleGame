using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip audioclip)
    {
        source.clip = audioclip;
        source.Play();
    }

}
