using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static event Action onChangeValues;
    public static event Action onStartValues;

    public GameObject[] playersHide = new GameObject[4];

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (onStartValues != null)
            onStartValues();

        SumirPlayers();
    }
    private void Update()
    {
        if (onChangeValues != null)
            onChangeValues();
    }

    void SumirPlayers()
    {
        for (int i = 0; i < playersHide.Length; i++)
        {
            if (playersHide[i].transform.GetChild(0).GetComponent<Image>().sprite == null)
            {
                playersHide[i].SetActive(false);
            }
        }
    }

    public void SumirTudo()
    {
        for (int i = 0; i < playersHide.Length; i++)
        {
            playersHide[i].SetActive(false);
        }
    }
}
