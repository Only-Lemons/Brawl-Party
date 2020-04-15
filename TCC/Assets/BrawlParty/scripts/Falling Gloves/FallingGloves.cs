using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGloves : MiniGame
{

    List<PlayerController> players = new List<PlayerController>();
    float timeOfGame = 30;
    GameObject[] hammers = new GameObject[6];
    List<PlayerController> winners = new List<PlayerController>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    bool adicionolPoint = false;
    int numwinner = 0;
    float lasthit = 0;

    int dificuldade = 0;

    int pontoTotal;
    float tempoVerificacao;
    bool tempoIniciar = false;
    bool falha = false;
    int qtdVivos;

    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        
        if(GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);


        foreach (var player in players)
        {
            player.actualGameMode = this;

        }

        falha = false;
        tempoIniciar = false;
        tempoVerificacao = 0.3f;
        InsertPlayerInDates();
        InsertHammersInDates();
        
        //GameController.singleton.uIManager.SumirTudo();

        pontoTotal = players.Count;

    }


    void Update()
    {
        ContarFalha();

        if (tempoVerificacao < 0 && qtdVivos < 1)
        {
            falha = true;
            WinRule();
        }
        else if (tempoVerificacao < 0 && qtdVivos >= 1)
            WinRule();

        if (!adicionolPoint)
        {
            lasthit -= Time.deltaTime;
            timeOfGame -= Time.deltaTime;
            ShowTime();
            if (timeOfGame <= 0)
            {
                InsertWinners();

                WinRule();
            }
            if (lasthit <= 0)
            {
                Falling();
                lasthit = 5 - (dificuldade / 6);
            }
        }
    }

    void Falling()
    {
        List<int> posicoes = new List<int>();
        posicoes.Clear();
        
        int random;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                random = Random.Range(0, hammers.Length);
            }
            while (posicoes.Contains(random));

            posicoes.Add(random);

            hammers[random].GetComponent<Animator>().SetTrigger("fall");
        }

        dificuldade++;
    }


    public override void Action(PlayerController player)
    {
        if (player.pulou == false )
        {
            player.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, 0.41f, player.gameObject.transform.position.z);
            player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.VelocityChange);
            player.pulou = true;
        }
    }

    public override void HitRule(PlayerController player)
    {
        player.gameObject.SetActive(false);
        playerMortos[player] = true;
        player.pontosGenericos -= pontoTotal;
        pontoTotal-=1;
        if (VerifyPlayerMortos())
        {
            winners.Add(player);
            InsertWinners();
            numwinner++;
            WinRule();
        }
    }

    public override void Jump(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void MovementRule(PlayerController player)
    {
       if (player._movementAxis.x > 0)
        {
            player.direc = 1;
            player.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.right), Quaternion.identity, Time.deltaTime);
        }
        else if (player._movementAxis.x < 0)
        {
            player.direc = -1;
            player.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.left), Quaternion.identity, Time.deltaTime);
        }
        else
        {
            player.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.zero), Quaternion.identity, Time.deltaTime);
        }

        player.transform.position += new Vector3(player._movementAxis.x, 0, 0) * player.speed * Time.deltaTime;
    }

    public override void PointRule(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void RotationRule(PlayerController player)
    {
        // Faz nada
    }

    public override void WinRule()
    {
        if (!adicionolPoint)
        {
            if (!falha)
            {
                for (int i = 0; i < winners.Count; i++)
                {
                      GameManager.Instance.playersPontos[winners[i].gameObject.transform.parent.gameObject] += 2;
                }
            }

            if (falha)
            {
                for (int i = 0; i <players.Count; i++)
                {
                     GameManager.Instance.playersPontos[players[i].gameObject.transform.parent.gameObject] += 1;
                }
            }
            
            adicionolPoint = true;
            GameManager.Instance.WinMinigame();
        }
    }
    void InsertPlayerInDates()
    {
        foreach (PlayerController player in players)
        {
            playerMortos.Add(player, false);
            player.pontosGenericos =players.Count;
        }
    }
    void InsertHammersInDates()
    {
        hammers = GameObject.FindGameObjectsWithTag("Brick");

    }

    void ContarFalha()
    {
        if (tempoIniciar)
        {
            tempoVerificacao -= Time.deltaTime;
        }

    }
    
    bool VerifyPlayerMortos()
    {
        int a = 0;
        bool boleano;

        for (int i = 0; i < playerMortos.Count; i++)
        {
            if (playerMortos[players[i]] == false)
                a++;
        }

        if (a <= 1)
        {
            boleano = true;
            tempoIniciar = true;
        }
        else boleano = false;

        qtdVivos = a;

        return boleano;
    }

    void InsertWinners()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!playerMortos[players[i]])
            {
                winners.Add(players[i]);
            }
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00");
        string seconds = ((int)(timeOfGame % 60)).ToString("00");
        //aux.time.text = minute + ":" + seconds;
    }
}
