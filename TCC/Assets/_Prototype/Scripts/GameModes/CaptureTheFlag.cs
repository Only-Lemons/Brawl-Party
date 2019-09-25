using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CaptureTheFlag : IGameMode
{
    GameController aux;
    List<PlayerController> pl = new List<PlayerController>();
    public Dictionary<PlayerController, int> pontos = new Dictionary<PlayerController, int>();
    public float actualtime = 5;

    public GameObject flag;
    
    bool withFlag;

    public CaptureTheFlag(GameController gameController,float time)
    {
        flag = GameObject.Find("Flag");
        aux = gameController;
        actualtime = time;
    }
    public void StartGame()
    {
        GameObject[] ax = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject g in ax)
        {
            pl.Add(g.GetComponent<PlayerController>());
        }
    }
    public void FinishGame()
    {
        actualtime -= Time.deltaTime;
        WhoPlayerFlag(pl);
        if(actualtime <= 0)
            WinRule();
    }
    public void PointRule(PlayerController player)
    {
        pontos[player] += 1;
        player.playerUI.points.text = pontos[player].ToString();
    }
    public void WinRule()
    {

    }
    public void DeathRule(PlayerController player)
    {

    }

    void WithFlag(PlayerController p)
    {
        pontos[p] += (int)Time.deltaTime;
        p.playerUI.points.text = pontos[p].ToString();
        flag.transform.position = p.transform.position;
    }

    void WhoPlayerFlag(List<PlayerController> player)
    {
        WithFlag(player.Find(x =>  x.withFlag == true)); 
    }
}
