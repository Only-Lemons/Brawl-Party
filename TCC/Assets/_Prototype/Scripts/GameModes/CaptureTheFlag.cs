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



    public CaptureTheFlag(GameController gameController, float time)
    {
        auxp = null;
        aux = GameController.Singleton;
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
        if (actualtime <= 0)
            WinRule();
    }
    void AddPlayerPoints()
    {
        foreach (PlayerController player in aux.playerManager.Players)
        {
            pontos.Add(player, 0);
            bandeira.Add(player, false);
            player.playerUI.points.text = pontos[player].ToString();
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(actualtime / 60)).ToString("00"); ;
        string seconds = ((int)(actualtime % 60)).ToString("00"); ;

        aux.time.text = minute + ":" + seconds;
    }

    public void PointRule(PlayerController player)
    {
        if (bandeira[player] == false)
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
            pontos[auxp] += 0.1f;
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
            aux.playerManager.playerMortos.Add(player, timeToRespawn);
            aux.playerManager.playerMortosPrefabs.Add(player);
            player.playerUI.Respawn.enabled = true;
            Vector3 posAux = player.transform.position;


            if (bandeira[player])
            {
                auxp = null;
                bandeira[player] = false;
                //GameObject.Instantiate(flag, posAux + new Vector3(Random.Range(-1,2), 1, Random.Range(-1, 2)), Quaternion.identity);
                GameObject.Instantiate(flag, Vector3.zero, Quaternion.identity);

                Debug.Log("Entrei na bandeira");
            }
            //player.gameObject.transform.position = new Vector3(300, 0, 300);
            player.gameObject.SetActive(false);
        }



    }




}
