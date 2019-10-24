using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameConfig : MonoBehaviour
{
    public Slider quantidyGames;
    public Slider timeInGames;


    public Text quantGames;
    public Text typeOfTime;

    private void Start()
    {
        quantidyGames.value = GameManager.Instance.quantGames;
        timeInGames.value = GameManager.Instance.TimeInGame;
    }
    void Update()
    {
        GameManager.Instance.quantGames = (int)quantidyGames.value;
        quantGames.text = quantidyGames.value.ToString("0");

        GameManager.Instance.TimeInGame = (int)timeInGames.value;
        switch (timeInGames.value)
        {
            case 1:
                typeOfTime.text = "Rápido";
                break;
            case 2:
                typeOfTime.text = "Medio";
                break;
            case 3:
                typeOfTime.text = "Lento";
                break;
        }

    }
}
