using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridironGauntlet : IGameMode
{
    GameController aux;
    float timeOfGame;
    GameObject _Monster = Resources.Load("Mecanicas/Monster") as GameObject;
    Dictionary<PlayerController, int> vidasPlayer = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, bool> canDamage = new Dictionary<PlayerController, bool>();
    List<Vector3> pointsInicial = new List<Vector3>();
    List<Vector3> pointsFinais = new List<Vector3>();
    List<PlayerController> winners = new List<PlayerController>();
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    bool adicionolPoint = false;
    int numwinner = 0;
    
    public GridironGauntlet(GameController gameController,float time)
    {
        aux = gameController;
        timeOfGame = time;
    }
    public void Action(PlayerController player)
    {

    }
    IEnumerator blinkPlayer(PlayerController player)
    {
        canDamage[player] = false;
        player.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        player.gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        player.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        player.gameObject.GetComponent<MeshRenderer>().enabled = true;
        yield return new WaitForSeconds(0.3f);
        player.gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.3f);
        player.gameObject.GetComponent<MeshRenderer>().enabled = true;
        canDamage[player] = true;
    }
    public void HitRule(PlayerController player)
    {
        if (canDamage[player])
        {
           
            vidasPlayer[player] -= 1;
            if (vidasPlayer[player] == 0)
            {
                if (VerifyPlayerMortos())
                {
                    winners.Add(player);
                    numwinner++;
                    WinRule();
                }
                else
                {
                    player.ResetarPlayer();
                    player.gameObject.SetActive(false);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    blinkPlayer(player);
            }
        }
    }
    bool VerifyPlayerMortos()
    {
        int a = 0;
        for (int i = 0; i < playerMortos.Count; i++)
        {
            if (playerMortos[GameController.singleton.playerManager.playersControllers[i]] == false)
                a++;
        }
        if (a > 1)
            return false;

        return true;
    }
    public void Update()
    {
        if (!adicionolPoint)
        {
            timeOfGame -= Time.deltaTime;
            ShowTime();
            if (timeOfGame <= 0)
            {
                InsertWinners();
                WinRule();
            }
        }
    }
    void IntanceObject() { 
    
    }
    void InsertWinners()
    {
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if (playerMortos[aux.playerManager.playersControllers[i]])
            {
                winners.Add(aux.playerManager.playersControllers[i]);
            }
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00"); ;
        string seconds = ((int)(timeOfGame % 60)).ToString("00"); ;
        aux.time.text = minute + ":" + seconds;
    }
    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        player.position += -dir * speed * Time.deltaTime;
    }

    public void PointRule(PlayerController player)
    {

    }

    public void RotationRule(Vector3 dir, Transform player)
    {

    }

    public void StartGame()
    {
        InsertPlayerInDates();
    }
    void InsertPlayerInDates()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            canDamage.Add(player, true);
            vidasPlayer.Add(player, 3);
            playerMortos.Add(player, false);
        }
    }
    public void WinRule()
    {
        if (!adicionolPoint)
        {
            for (int i = 0; i < winners.Count; i++)
            {
                GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(winners[i])] += 1;
            }
            aux.FinishGame();
            adicionolPoint = true;
        }
    }
}
