using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("Áudios")]
    public AudioClip musicGame;
    public AudioClip clipColeta;
    public AudioClip clipMorte;
    public AudioClip clipHit;

    void Awake()
    {
        instance = this;
    }
}
