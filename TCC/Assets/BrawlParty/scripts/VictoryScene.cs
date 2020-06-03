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
        if (GameManager.Instance.quantGames > GameManager.Instance.quantTGames)
        {

            GameManager.Instance.nextLevel = 10;
        }
        foreach (GameObject panel in GameManager.Instance.playersPanels)
        {
            panel.GetComponentInChildren<PlayerSelect>().isConfirmed = true;
        }

        AtualizarSprites();
        updateScore();

    }

    void Update()
    {
        ConfirmarTutorial();
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

        for (int i = 0; i < playerScoreBoard.Length; i++)
        {
            if (playerScoreBoard[i].name.text == "Nome")
            {
                playerScoreBoard[i].icon.sprite = null;
                playerScoreBoard[i].name.text = "";
                playerScoreBoard[i].pontos.text = "";
            }
        }
    }

    void ConfirmarTutorial()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if (!GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().isConfirmed)
            {
                confirmPlayer[i].text = "OK!";
                AudioController.Instance.PlayAudio("EnterPlayer");
            }
            else
            {
                confirmPlayer[i].text = "Waiting...";
            }
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.end = true;
    }

    private void OnDisable()
    {
        GameManager.Instance.end = false;
    }
}
