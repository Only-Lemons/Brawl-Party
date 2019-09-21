using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAll : IGameMode
{
    GameController aux;
    float time;
    public float actualtime;
    public Dictionary<PlayerController, int> pontos = new Dictionary<PlayerController, int>();
    public float timeToRespawn = 3f;
    public FreeForAll(GameController gameController,float time)
    {
        aux = gameController;
        this.time = time;
        actualtime = 0;
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
                player.ResetarPlayer();
                player.gameObject.SetActive(false);
                PointRule(auxp);
            }
        }
     
    }

    public void FinishGame()
    {
        actualtime += Time.deltaTime;
        if(actualtime > time)
            WinRule();
    }

    public void PointRule(PlayerController player)
    {
        //conta pontos toda vez que o player matar alguem
      
        pontos[player] += 1;
        Debug.Log(player.gameObject.name + "  tem " + pontos[player] + "pontos");

    }

    public void StartGame()
    {
        AddPlayerPoints();
    }

    void AddPlayerPoints()
    {
        foreach (PlayerController player in aux.playerManager.Players)
        {
            pontos.Add(player,0);
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
        Debug.Log("Acabooou!!! e o ganhador foi : " + playerMaior.gameObject.name + "com "  + maiorPonto +" pontos");
    }
}
