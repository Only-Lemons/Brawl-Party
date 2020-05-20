using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static void PlayGameMusic()
    {
        try
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(AudioController.instance.musicGame);
            audioSource.loop = true;
        }
        catch (Exception e)
        {
            Debug.Log("Erro ao reproduzir PlayGameMusic: " + e.Message);
        }
    }
    public static void PlayColeta()
    {
        try
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(AudioController.instance.clipColeta);
        }
        catch (Exception e)
        {
            Debug.Log("Erro ao reproduzir PlayColeta: " + e.Message);
        }
    }

    public static void PlayMorte()
    {
        try
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(AudioController.instance.clipMorte);
        }
        catch (Exception e)
        {
            Debug.Log("Erro ao reproduzir PlayMorte: " + e.Message);
        }
    }

    public static void PlayHit()
    {
        try
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(AudioController.instance.clipHit);
        }
        catch (Exception e)
        {
            Debug.Log("Erro ao reproduzir PlayHit: " + e.Message);
        }
    }
}
