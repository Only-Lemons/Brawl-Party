using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct playerScore
{
    public Image icon;
    public Text name;
    public Text pontos;
}


public class VictoryScene : MonoBehaviour
{

    public Image[] spritePlayer;
    public Text[] confirmPlayer;

    public playerScore[] playerScoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject panel in GameManager.Instance.playersPanels)
        {
            panel.GetComponentInChildren<PlayerSelect>().isConfirmed = true;
        }
        AtualizarSprites();
        updateScore();
    }


    void updateScore()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            playerScoreBoard[i].icon.sprite = GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().selectSprite;
            playerScoreBoard[i].name.text = GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().selectName;
            playerScoreBoard[i].pontos.text = GameManager.Instance.playersPontos[GameManager.Instance.playersPanels[i]].ToString();

        }

    }


    void AtualizarSprites()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            spritePlayer[i].sprite = GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().selectSprite;
        }
         
    }

    void ConfirmarTutorial()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if (!GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().isConfirmed)
                confirmPlayer[i].text = "OK!";
            else
                confirmPlayer[i].text = "Waiting...";
        }
    }
}
