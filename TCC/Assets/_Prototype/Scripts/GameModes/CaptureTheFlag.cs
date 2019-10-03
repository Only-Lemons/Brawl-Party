using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaptureTheFlag : IGameMode
{
    GameController aux;
    PlayerController auxp = null;
    public Dictionary<PlayerController, float> pontos = new Dictionary<PlayerController, float>();
    public Dictionary<PlayerController, bool> bandeira = new Dictionary<PlayerController, bool>();
    public float actualtime = 5;
    GameObject flag = Resources.Load("Mecanicas/Flag") as GameObject;
    public float timeToRespawn = 3;



    public CaptureTheFlag(GameController gameController,float time)
    {
        auxp = null;
        aux = gameController;
        actualtime = time;
    }
    public void StartGame()
    {
        GameObject.Instantiate(flag, new Vector3(0, 1, 0), Quaternion.identity);
        AddPlayerPoints();
    }
    public void FinishGame()
    {
        actualtime -= Time.deltaTime;
        ShowTime();
        AddPoints();
        if(actualtime <= 0)
            WinRule();
    }
     void AddPlayerPoints()
    {
        foreach (PlayerController player in aux.playerManager.Players)
        {
            pontos.Add(player,0);
            bandeira.Add(player,false);
            player.playerUI.points.text = pontos[player].ToString();
        }
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
        if(bandeira[player] == false)
        {
            bandeira[player] = true;
            auxp = player;
            foreach (PlayerController playerm in GameController.Singleton.playerManager.Players)
            {
                if (playerm != player)
                    bandeira[playerm] = false;
            }
        }
        
        
       
    }
    void AddPoints()
    {
        if (auxp != null)
        {
            pontos[auxp] += 0.5f;
            auxp.playerUI.points.text = pontos[auxp].ToString("0");
        }
    }
    public void WinRule()
    {
        Time.timeScale = 0;
    }
    public void DeathRule(PlayerController player)
    {
         if (player.canDeath)
            {
                if(bandeira[player]){
                    auxp = null;
                    bandeira[player] = false;
                    GameObject.Instantiate(flag,player.transform.position, Quaternion.identity);
                }
                aux.playerManager.playerMortos.Add(player, timeToRespawn);
                aux.playerManager.playerMortosPrefabs.Add(player);
                player.playerUI.Respawn.enabled = true;
                player.gameObject.SetActive(false);
            }
      


    }

   

 
}
