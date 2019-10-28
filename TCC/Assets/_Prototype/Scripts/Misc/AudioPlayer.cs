using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> audios = new List<AudioClip>();
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayIndex(int index)
    {
        audioSource.clip = audios[index];
        audioSource.Play();
    }


}
