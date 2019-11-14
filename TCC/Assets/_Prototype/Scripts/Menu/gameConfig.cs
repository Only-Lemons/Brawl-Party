using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameConfig : MonoBehaviour
{
    public Slider quantidyGames;
    public Slider timeInGames;


    public Text quantGames;
    public Text typeOfTime;

    private void Start()
    {
        quantidyGames.maxValue = GameManager.Instance.quantTGames;
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
                typeOfTime.text = "30 à 40 segundos";
                break;
            case 2:
                typeOfTime.text = "60 à 80 segundos";
                break;
            case 3:
                typeOfTime.text = "90 à 120 segundos";
                break;
        }
    }

    public void mudarScena()
    {
        GameManager.Instance.nextLevel = 5;
        SceneManager.LoadScene(14);
    }
}
