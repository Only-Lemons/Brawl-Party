using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItRock : IGameMode
{
    GameController aux;
    float timeOfGame;
    GameObject[] hammers = new GameObject[6];
    List<PlayerController> winners = new List<PlayerController>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    bool adicionolPoint = false;
    int numwinner = 0;
    float lasthit = 2;
    public GetItRock(GameController gameController, float time)
    {
        aux = gameController;
        timeOfGame = time;
    }
    public void HitRule(PlayerController player)
    {
        player.gameObject.SetActive(false);
        playerMortos[player] = true;
        if (VerifyPlayerMortos())
        {
            //winners.Add(player);
            InsertWinners();
            numwinner++;
            //WinRule();
        }

        
    }
    void fallRock()
    {

        List<int> posicoes = new List<int>();
        posicoes.Clear();
        int HammerQuant = Random.Range(1, hammers.Length - 1);
        for (int i = 0; i < HammerQuant; i++)
        {
            int hammer = (int)(Random.value * (hammers.Length));
            while (posicoes.Contains(hammer))
            {
                hammer = (int)(Random.value * (hammers.Length));
            }
            posicoes.Add(hammer);
            //TO-DO hammers[hammer].getComponent<Animation>()...

        }
        for (int i = 0; i < posicoes.Count; i++)
        {
            hammers[posicoes[i]].GetComponent<Animator>().SetTrigger("fall");
        }
    }
    public void Update()
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
                fallRock();
                lasthit = 2;
            }
        }
    }
    void InsertWinners()
    {
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if (!playerMortos[aux.playerManager.playersControllers[i]])
            {
                winners.Add(aux.playerManager.playersControllers[i]);
            }
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00");
        string seconds = ((int)(timeOfGame % 60)).ToString("00");
        aux.time.text = minute + ":" + seconds;
    }
    bool VerifyPlayerMortos()
    {
        int a = 0;
        bool boleano;

        for (int i = 0; i < playerMortos.Count; i++)
        {
            if (playerMortos[GameController.singleton.playerManager.playersControllers[i]] == false)
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

    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        if (dir.x > 0)
        {
            player.gameObject.GetComponent<PlayerController>().direc = 1;
            player.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.right), Quaternion.identity, Time.deltaTime);
        }
        else if (dir.x < 0)
        {
            player.gameObject.GetComponent<PlayerController>().direc = -1;
            player.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.left), Quaternion.identity, Time.deltaTime);
        }
        else
        {
            player.rotation = Quaternion.Lerp(Quaternion.LookRotation(Vector3.zero), Quaternion.identity, Time.deltaTime);
        }

        player.transform.position += new Vector3(dir.x, 0, 0) * speed * Time.deltaTime;


        //player.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, -7.5f, 6f),player.position.y,player.position.z);
    }


    public void PointRule(PlayerController player)
    {

    }

    public void RotationRule(Vector3 dir, Transform player)
    {

    }

    public void StartGame()
    {
        falha = false;
        tempoIniciar = false;
        tempoVerificacao = 0.3f;
        InsertPlayerInDates();
        InsertHammersInDates();
        GameController.singleton.uIManager.SumirTudo();
    }
    void InsertHammersInDates()
    {
        hammers = GameObject.FindGameObjectsWithTag("Brick");

    }
    void InsertPlayerInDates()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            //player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
            playerMortos.Add(player, false);
        }
    }

    float tempoVerificacao;
    bool tempoIniciar = false;
    bool falha = false;
    int qtdVivos;

    void ContarFalha()
    {
        if (tempoIniciar)
        {
            tempoVerificacao -= Time.deltaTime;
        }

    }

    public void WinRule()
    {
        if (!adicionolPoint)
        {
            for (int i = 0; i < winners.Count; i++)
            {
                if (!falha)
                    GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(winners[i])] += 1;
            }
            aux.FinishGame();
            adicionolPoint = true;
        }
    }

    public void Action(PlayerController player)
    {
        if (player.pulou == false && GameController.singleton.comecou)
        {
            player.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, 0.41f, player.gameObject.transform.position.z);
            player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10f, ForceMode.VelocityChange);
            player.pulou = true;
        }
    }
}