using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using DG.Tweening;

[System.Serializable]
public struct playerScore
{
    public Image icon;
    public Text name;
    public Text pontos;
    public RectTransform posScore;
    public int posAtual;
}


public class VictoryScene : MonoBehaviour
{

    public Image[] spritePlayer;
    public Text[] confirmPlayer;
    public List<float> positionsScore = new List<float>();
    public List<float> positionsScoreLerp = new List<float>();

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
        PointsSet();
    }

    void Update()
    {
        ConfirmarTutorial();
        UpdatePointsVictory();
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

    void PointsSet()
    {
        //seta as listas
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            playerScoreBoard[i].posAtual = i;
            positionsScore.Add(playerScoreBoard[i].posScore.localPosition.y); //valor real para manter guardado
            positionsScoreLerp.Add(0); //valor 0 para efeito
        }
    }

    void UpdatePointsVictory()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            for (int j = 0; j < GameManager.Instance.playersPanels.Count; j++)
            {
                if (i != j)
                {
                    if (int.Parse(playerScoreBoard[i].pontos.text) > int.Parse(playerScoreBoard[j].pontos.text))
                    {
                        if (playerScoreBoard[i].posAtual < playerScoreBoard[j].posAtual)
                        {
                            int aux = playerScoreBoard[j].posAtual;
                            playerScoreBoard[j].posAtual = playerScoreBoard[i].posAtual;
                            playerScoreBoard[i].posAtual = aux;

                            float auxT = positionsScore[j];
                            positionsScore[j] = positionsScore[i];
                            positionsScore[i] = auxT;
                        }
                    }
                }
            }
        }

        //faz a transição suave entre os paineis
        for (int i = 0; i < positionsScoreLerp.Count; i++)
        {
            positionsScoreLerp[i] = Mathf.Lerp(positionsScoreLerp[i], positionsScore[i], Time.fixedDeltaTime);
        }

        //seta o valor da posição
        for (int i = 0; i < positionsScore.Count; i++)
        {
            playerScoreBoard[i].posScore.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, positionsScoreLerp[i], 100);
            //playerScoreBoard[i].posScore.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, Mathf.Lerp(playerScoreBoard[i].posScore.localPosition.y, positionsScore[i], Time.fixedDeltaTime), 100);
        }
    }
}
