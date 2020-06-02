using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGameController : MonoBehaviour
{
    public static TimeGameController Instance;

    //Time
    float timeComecar = 3;
    float timePreFim = 1.5f;

    //UI
    public Text inicio;
    public Text fim;

    //Bool
    public bool comecou = false;
    public bool acabou = false;
    public bool acabouMsm = false;
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        Comecar();
        Acabar();
    }

    public bool Comecou()
    {
        return comecou;
    }
    public bool Acabou()
    {
        return acabou;
    }
    public bool AcabouMesmo()
    {
        return acabouMsm;
    }

    void Comecar()
    {
        timeComecar -= Time.deltaTime;
        inicio.text = ((int)timeComecar +1).ToString("0");
        if (timeComecar <= 0)
        {
            inicio.text = "GO!";
            comecou = true;
            if(timeComecar < -1.5f)
                inicio.text = "";
            return;
        }
    }

    void Acabar()
    {
        if (!acabou)
            return;

        fim.text = "FINISH!";
        timePreFim -= Time.deltaTime;
        if (timePreFim <= 0)
        {
            fim.text = "";
            acabouMsm = true;
        }
    }

}
