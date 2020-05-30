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

    public string MenuPersonagem;

    private void Start()
    {
        quantidyGames.maxValue = GameManager.Instance.quantTGames-1;
        quantidyGames.value = GameManager.Instance.quantGames;
        timeInGames.value = GameManager.Instance.TimeInGame;
    }
    void Update()
    {
        GameManager.Instance.quantTGames = (int)quantidyGames.value;
        quantGames.text = quantidyGames.value.ToString("0");


        GameManager.Instance.TimeInGame = (int)timeInGames.value;
        switch (timeInGames.value)
        {
            case 1:
                typeOfTime.text = "30 to 40 seconds";
                break;
            case 2:
                typeOfTime.text = "60 to 80 seconds";
                break;
            case 3:
                typeOfTime.text = "90 to 120 seconds";
                break;
        }
    }

    public void mudarScena()
    {
        GameManager.Instance.nextLevel = 8;
        SceneManager.LoadScene(MenuPersonagem);
    }
}
