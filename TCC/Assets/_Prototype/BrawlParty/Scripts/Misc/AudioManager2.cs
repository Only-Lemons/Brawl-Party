using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip deathAudio;
    public AudioClip endRoundAudio;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playDeath()
    {
        audioSource.clip = deathAudio;
        audioSource.Play();
    }

    public void playEndRound()
    {
        audioSource.clip = endRoundAudio;
        audioSource.Play();
    }

}
