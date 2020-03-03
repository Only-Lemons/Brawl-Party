using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeConfigs : MonoBehaviour
{
    [Header ("Volume Settings")]
    public GameObject[] volumeObjs = new GameObject[8];

    public GameObject[] toggles;

    public Image bgToogle;

    public Sprite spriteOn, spriteOff;

    public AudioMixer volume;
    private int aux = 7,
                vol = 0;

    public string nomeParametro;



    public void volumeUp()
    {
        if (aux < 7)
        {
            aux++;
            vol = vol + 4;
        }

        this.volumeObjs[aux].SetActive(true);
        volume.SetFloat(nomeParametro, vol);
    }

    public void volumeDown()
    {
        aux--;
        vol = vol - 4;

        if (aux < 0)
        {
            aux = 0;
            vol = -28;
        }

        this.volumeObjs[aux + 1].SetActive(false);
        volume.SetFloat(nomeParametro, vol);
    }


    public void mute(GameObject go)
    {

        if(go.transform.GetChild(0).gameObject.activeSelf)
        {
            volume.SetFloat(nomeParametro, -28);
            go.transform.GetChild(0).gameObject.SetActive(false);
            go.transform.GetChild(1).gameObject.SetActive(true);
            bgToogle.sprite = spriteOff;
        }
        else{
            volume.SetFloat(nomeParametro, vol);
            go.transform.GetChild(1).gameObject.SetActive(false);
            go.transform.GetChild(0).gameObject.SetActive(true);
            bgToogle.sprite = spriteOn;
        }
      
    }




}
