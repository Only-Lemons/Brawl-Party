using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class FreeForAll : IGameMode
{
    GameController aux;
  
    public float actualtime;
    public Dictionary<PlayerController, int> pontos = new Dictionary<PlayerController, int>();
    public float timeToRespawn = 3f;
    public FreeForAll(GameController gameController,float time)
    {
        aux = gameController;
        actualtime = time;
    }

    public void DeathRule(PlayerController player)
    {
        //Renasce dps de 3 seg, em sua base.
        if (player.playerLastDamage != null)
        {
            if (player.canDeath)
            {
                PlayerController auxp = player.playerLastDamage;
                aux.playerManager.playerMortos.Add(player, timeToRespawn);
                aux.playerManager.playerMortosPrefabs.Add(player);
                player.playerUI.Respawn.enabled = true;
                player.gameObject.SetActive(false);
                PointRule(auxp);
            }
        }
     
    }

    public void FinishGame()
    {
        actualtime -= Time.deltaTime;
        ShowTime();
        if(actualtime <= 0)
            WinRule();
    }

    public void ShowTime()
    {
        string minute;
        string seconds;
        if(actualtime / 60 < 1)
        {
            minute = "00";
            if (actualtime  < 10)
            {
                seconds = "0" + (actualtime).ToString("0");
            }
            else
            {
                seconds = (actualtime).ToString("0");
            }
        }
        else
        {
            if (actualtime / 60 > 10) {
                minute = (actualtime / 60 -1).ToString("0");
                seconds = (actualtime % 60).ToString("0");
            }
            else
            {
                minute = "0"+(actualtime / 60 -1).ToString("0");
                if ((actualtime - (actualtime / 60)) < 10)
                {
                    seconds ="0"+ (actualtime % 60).ToString("0");
                }
                else
                {
                    seconds = (actualtime % 60).ToString("0");
                }
            }
        }
        aux.time.text = minute +":"+ seconds;

    }

    public void PointRule(PlayerController player)
    {
        //conta pontos toda vez que o player matar alguem
      
        pontos[player] += 1;
        player.playerUI.points.text = pontos[player].ToString();
        

    }

    public void StartGame()
    {
        AddPlayerPoints();
        aux.playerManager.timeRespawn = timeToRespawn;
    }

    void AddPlayerPoints()
    {
        foreach (PlayerController player in aux.playerManager.Players)
        {
            pontos.Add(player,0);
            player.playerUI.points.text = pontos[player].ToString();
        }
    }


    public void WinRule()
    {
        PlayerController playerMaior = null;
        int maiorPonto = int.MinValue;
        foreach (PlayerController player in aux.playerManager.Players)
        {
            if (pontos[player] > maiorPonto)
            {
                maiorPonto = pontos[player];
                playerMaior = player;
            }
        }
        Time.timeScale = 0;
        SceneManager.LoadScene(1);
     
        
    }

}
