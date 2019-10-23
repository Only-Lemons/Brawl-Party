using UnityEngine;
using System.Collections.Generic;

public class JhonBeen : IGameMode
{
    public class postionsLR{
       public Vector3 left;
       public Vector3 right;
    }
    GameController aux;
    float timeOfGame;
    GameObject _bird = Resources.Load("Mecanicas/Bird") as GameObject;
    PlayerController[] winners = new PlayerController[4];
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, postionsLR> playerPosition = new Dictionary<PlayerController, postionsLR>();
    int numwinner = 0;
    bool adicionolPoint = false;
    public JhonBeen(GameController gameController, float time)
    {
        timeOfGame -= time;
        aux = gameController;
    }
    public void DeathRule(PlayerController player)
    {
        player.ResetarPlayer();
        player.gameObject.SetActive(false);

        playerMortos[player] = true;
        if (VerifyPlayerMortos())
        {
            winners[numwinner] = player;
            numwinner++;
            WinRule();
        }
    }
    bool VerifyPlayerMortos()
    {
        for (int i = 0; i < playerMortos.Count; i++)
        {
            if (playerMortos[GameController.singleton.playerManager.playersControllers[i]] == false)
                return false;
        }
        return true;
    }
    public void FinishGame()
    {
        if (!adicionolPoint)
        {
            timeOfGame -= Time.deltaTime;
            ShowTime();
            goDownPlayers();
            if (timeOfGame <= 0)
            {
                InsertWinners();
                WinRule();
            }
            
        }
    }
    void InsertWinners()
    {
        int a = 0;
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if (playerMortos[aux.playerManager.playersControllers[i]])
            {
                winners[a] = aux.playerManager.playersControllers[i];
                a++;
            }
        }
    }

    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00"); ;
        string seconds = ((int)(timeOfGame % 60)).ToString("00"); ;
        aux.time.text = minute + ":" + seconds;
    }
    void goDownPlayers()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 3 * Time.deltaTime, player.transform.position.z);
        }
    }
    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        if(dir.x > 0.4f)
        {
            player.position = new Vector3(playerPosition[player.gameObject.GetComponent<PlayerController>()].right.x, player.transform.position.y, player.transform.position.z);
        }else if(dir.y < 0.4f)
        {
            player.position = new Vector3(playerPosition[player.gameObject.GetComponent<PlayerController>()].left.x, player.transform.position.y, player.transform.position.z);
        }
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
        for(int i = 0;i < GameController.singleton.playerManager.playersControllers.Count;i++)
        {
            playerMortos.Add(GameController.singleton.playerManager.playersControllers[i], false);
            playerPosition[GameController.singleton.playerManager.playersControllers[i]].left = aux.tileManager.bases[i];
            playerPosition[GameController.singleton.playerManager.playersControllers[i]].right = new Vector3(aux.tileManager.bases[i].x + 1, aux.tileManager.bases[i].y, aux.tileManager.bases[i].z );
        }
    }

    public void WinRule()
    {
        if (!adicionolPoint)
        {
            for (int i = 0; i < winners.Length; i++)
            {
                GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(winners[i])] += 1;

            }
            aux.FinishGame();
            adicionolPoint = true;
        }
    }
}
