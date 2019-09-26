using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaptureTheFlag : IGameMode
{
    GameController aux;
    public Dictionary<PlayerController, int> pontos = new Dictionary<PlayerController, int>();
    public Dictionary<PlayerController, bool> bandeira = new Dictionary<PlayerController, bool>();
    public float actualtime = 5;
    PlayerController auxp;

    GameObject flag = Resources.Load("Mecanicas/Flag") as GameObject;

    
    bool withFlag;

    public CaptureTheFlag(GameController gameController,float time)
    {
        flag = GameObject.Find("Flag");
        aux = gameController;
        actualtime = time;
    }
    public void StartGame()
    {
        GameObject.Instantiate(flag, new Vector3(0, 0, 0), Quaternion.identity);
    }
    public void FinishGame()
    {
        actualtime -= Time.deltaTime;
        AddPoints();
        if(actualtime <= 0)
            WinRule();
    }
    public void PointRule(PlayerController player)
    {
        if(!bandeira[player])
        {
            bandeira[player] = true;
            auxp = player;
            foreach (PlayerController playerm in aux.playerManager.Players)
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
            pontos[auxp] += 1;
            auxp.playerUI.points.text = pontos[auxp].ToString();
        }
    }
    public void WinRule()
    {

    }
    public void DeathRule(PlayerController player)
    {
        bandeira[player] = false;
        auxp = null;
        GameObject.Instantiate(flag,player.transform.position, Quaternion.identity);


    }

    void WithFlag(PlayerController p)
    {
        pontos[p] += (int)Time.deltaTime;
        p.playerUI.points.text = pontos[p].ToString();
        flag.transform.position = p.transform.position;
    }

 
}
