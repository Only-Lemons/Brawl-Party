using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public  event Action onChangeValues;
    public  event Action onStartValues;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (onStartValues != null)
            onStartValues();
    }


    private void Update()
    {
        if (onChangeValues != null)
            onChangeValues();
    }


}
