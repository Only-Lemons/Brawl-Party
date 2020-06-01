using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingGloves : MiniGame
{

    List<PlayerController> players = new List<PlayerController>();
    public List<playerFalling> playersUI = new List<playerFalling>();

    float timeOfGame = 30;
    GameObject[] hammers = new GameObject[6];
    List<PlayerController> winners = new List<PlayerController>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, int> playersVidas = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, bool> playersI = new Dictionary<PlayerController, bool>();

    int numwinner = 0;
    float lasthit = 0;

    int dificuldade = 0;

    int pontoTotal;
    float tempoVerificacao;
    bool tempoIniciar = false;

    int qtdVivos;

    Vector3 posAllPlayerInit;
    float forceJump;

    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        posAllPlayerInit = players[0].transform.position;
        forceJump = 10;

        if (GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);


        foreach (var player in players)
        {
            player.actualGameMode = this;

        }

        /// isso tem que estar em todos minigames
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if (i < players.Count)
            {
                players[i].setColor(GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor);
            }
        }


        tempoIniciar = false;
        tempoVerificacao = 0.3f;
        InsertPlayerInDates();
        InsertHammersInDates();

        UIInit();

        //GameController.singleton.uIManager.SumirTudo();
        qtdVivos = players.Count;
        pontoTotal = players.Count;



    }


    void Update()
    {
        if (!TimeGameController.Instance.Comecou())
            return;

        ContarFalha();
    
        if (tempoVerificacao < 0 && qtdVivos < 1)
        {

            TimeGameController.Instance.acabou = true;
            //Invoke("WinRule",0);

        }
        else if (tempoVerificacao < 0 && qtdVivos >= 1)
        {
            TimeGameController.Instance.acabou = true;
            //Invoke("WinRule", 0);

        }


        lasthit -= Time.deltaTime;
        // timeOfGame -= Time.deltaTime;
        ShowTime();
        InsertWinners();
        //WinRule();
        if (lasthit <= 0)
        {
            Falling();
            lasthit = 5 - (dificuldade / 6);
        }

        WinRule();

    }

    void FixedUpdate()
    {
        Gravidade();
        Pequenino();
    }

    void AtualizarUI()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            for (int j = 0; j < 1; j++)
            {
                switch (playersVidas[players[i]])
                {
                    case 2:
                        playersUI[i].lifes[0].gameObject.SetActive(true);
                        playersUI[i].lifes[1].gameObject.SetActive(true);
                        break;
                    case 1:
                        playersUI[i].lifes[0].gameObject.SetActive(false);
                        playersUI[i].lifes[1].gameObject.SetActive(true);
                        break;
                    case 0:
                        playersUI[i].lifes[0].gameObject.SetActive(false);
                        playersUI[i].lifes[1].gameObject.SetActive(false);
                        break;
                
                } 
            }
        }
    }

    void UIInit()
    {
        for (int i = 0; i < playersUI.Count; i++)
        {
            if (i < GameManager.Instance.playersPanels.Count)
            {
                playersUI[i].icon.sprite = GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().selectSprite;
                for (int j = 0; j < 1; j++)
                {
                    if (j < playersVidas[players[i]])
                    {
                        playersUI[i].lifes[j].gameObject.SetActive(true);
                    }
                    else
                    {
                        playersUI[i].lifes[j].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                playersUI[i].pai.SetActive(false);
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
        if (player.transform.position.y <= 0.41f && !playersI[player])
        //if (player.pulou == false )
        {
            player.gameObject.transform.position = new Vector3(player.gameObject.transform.position.x, 0.41f, player.gameObject.transform.position.z);
            player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 2 * forceJump, ForceMode.VelocityChange);
            //player.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 10 * forceJump, ForceMode.Impulse);
            //player.pulou = true;
        }
    }

    public override void HitRule(PlayerController player)
    {
        if (playersI[player] == false)
        {
            playersVidas[player] -= 1;
            AtualizarUI();
            if (playersVidas[player] <= 0)
            {
                PointRule(player);
                qtdVivos--;
                //player.gameObject.SetActive(false); //teste
                playerMortos[player] = true;
                player.pontosGenericos -= pontoTotal;
                pontoTotal -= 1;
                if (VerifyPlayerMortos())
                {
                    winners.Add(player);
                    InsertWinners();
                    numwinner++;

                    TimeGameController.Instance.acabou = true;
                    //Invoke("WinRule", 0);
                }
            }
            playersI[player] = true;
            StartCoroutine(invencibilidadeCD(player));
        }
    }

    IEnumerator invencibilidadeCD(PlayerController player)
    {
        yield return new WaitForSeconds(2f);
        playersI[player] = false;
    }

    void Pequenino()
    {
        foreach (PlayerController p in players)
        {
            if (playersI[p])
                p.transform.localScale = Vector3.Lerp(p.transform.localScale, new Vector3(2.5f, 0.03f, 2.5f), Time.fixedDeltaTime * 10);
            else if(playerMortos[p])
                p.transform.localScale = Vector3.Lerp(p.transform.localScale, new Vector3(3f, 0.05f, 3f), Time.fixedDeltaTime * 10);
            else
                p.transform.localScale = Vector3.Lerp(p.transform.localScale, new Vector3(1, 1, 1), Time.fixedDeltaTime * 10);
        }
    }

    void Gravidade()
    {
        foreach (PlayerController p in players)
        {
            if (p.transform.position.y > 0)
            {
                p.transform.position = Vector3.Lerp(p.transform.position, new Vector3(p.transform.position.x, posAllPlayerInit.y, p.transform.position.z), Time.fixedDeltaTime * forceJump / 2);
            }
        }
    }

    public override void Jump(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void MovementRule(PlayerController player)
    {
        if (!playersI[player])
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
    }

    public override void PointRule(PlayerController player)
    {
        if (players.Count == 4)
        {
            switch (qtdVivos)
            {
                case 1:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 3;
                    break;
                case 2:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 2;
                    break;
                case 3:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 1;
                    break;
                default:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 0;
                    break;
            }
        }
        else
        {
            switch (qtdVivos)
            {

                case 1:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 1;
                    break;
                default:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 0;
                    break;
            }
        }


    }

    public override void RotationRule(PlayerController player)
    {
        // Faz nada
    }

    public override void WinRule()
    {
        //TimeGameController.Instance.acabou = true;
        if (TimeGameController.Instance.AcabouMesmo())
            GameManager.Instance.WinMinigame();

    }
    void InsertPlayerInDates()
    {
        foreach (PlayerController player in players)
        {
            playerMortos.Add(player, false);
            playersVidas.Add(player, 2);
            playersI.Add(player, false);
            player.pontosGenericos = players.Count;
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

        //qtdVivos = a;

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
[System.Serializable]
public struct playerFalling
{
    public GameObject pai;
    public Image icon;
    public Image[] lifes;
}