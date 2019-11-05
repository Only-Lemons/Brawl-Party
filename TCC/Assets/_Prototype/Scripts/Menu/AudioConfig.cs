using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioConfig : MonoBehaviour
{
    [Header ("Sliders")]
    public Slider GeralVolume;
    public Slider EffectsVolume;
    public Slider MusicVolume;
    [Header("Mixer")]
    public AudioMixer audioGeral;
 
    void Start()
    {
        if (audioGeral.GetFloat("Volume", out float value))
            GeralVolume.value = value;
        if (audioGeral.GetFloat("VolumeM", out float valueM))
            MusicVolume.value = valueM;
        if (audioGeral.GetFloat("VolumeE", out float valueE))
            EffectsVolume.value = valueE;
    }


    void Update()
    {
        audioGeral.SetFloat("Volume", GeralVolume.value);
        audioGeral.SetFloat("VolumeM", MusicVolume.value);
        audioGeral.SetFloat("VolumeE", EffectsVolume.value);
    }

    #region mute/unmute Geral
    public void muteAudioGeral()
    {
        audioGeral.SetFloat("Volume", 0);
    }

    public void unmuteAudioGeral()
    {
        audioGeral.SetFloat("Volume", GeralVolume.value);
    }
    #endregion

    #region mute/unmute Music
    public void muteAudioM()
    {
        audioGeral.SetFloat("VolumeM", 0);
    }

    public void unmuteAudioM()
    {
        audioGeral.SetFloat("VolumeM", MusicVolume.value);
    }
    #endregion

    #region mute/unmute Efeitos
    public void muteAudioE()
    {
        audioGeral.SetFloat("VolumeE", 0);
    }

    public void unmuteAudioE()
    {
        audioGeral.SetFloat("VolumeE", EffectsVolume.value);
    }
    #endregion
}
