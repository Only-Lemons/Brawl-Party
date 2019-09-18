using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeForAll : IGameMode
{
    GameController aux;
    float time;
    public float actualtime;
    public Dictionary<PlayerController, int> pontos = new Dictionary<PlayerController, int>();
    public FreeForAll(GameController gameController,float time)
    {
        aux = gameController;
        this.time = time;
        actualtime = 0;
    }

    public void DeathRule()
    {
       //Renasce dps de 3 seg, em sua base.
    }

    public void FinishGame()
    {
        actualtime += Time.deltaTime;
        if(actualtime > time)
            WinRule();
    }

    public void KillRule(PlayerController player)
    {
        //conta pontos toda vez que o player matar alguem
        pontos[player] += 1; 

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
        PlayerController playerMaior;
        int maiorPonto = int.MinValue;
        foreach (PlayerController player in aux.playerManager.Players)
        {
            if (pontos[player] > maiorPonto)
            {
                maiorPonto = pontos[player];
                playerMaior = player;
            }
        }
    }
}
