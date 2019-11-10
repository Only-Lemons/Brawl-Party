using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnackAtack : IGameMode
{
    class Stun
    {
       public bool canMove;
       public float timeInStun;
    }
    bool adicionolPoint = false;
    float timeOfGame, InstanceHiveTime, InstanceNutTime;
    GameController aux;
    Dictionary<PlayerController, Stun> canMove = new Dictionary<PlayerController, Stun>();
    Dictionary<PlayerController, int> point = new Dictionary<PlayerController, int>();
    GameObject _Nut = Resources.Load("Mecanicas/Noz") as GameObject;
    GameObject _Hive = Resources.Load("Mecanicas/Colmeia") as GameObject;
    GameObject _basket1 = Resources.Load("Mecanicas/Cesta1") as GameObject;
    GameObject _basket2 = Resources.Load("Mecanicas/Cesta2") as GameObject;
    GameObject _basket3 = Resources.Load("Mecanicas/Cesta3") as GameObject;
    public SnackAtack(GameController controller, float time)
    {
        aux = controller;
        timeOfGame = time;
    }
    public void Action(PlayerController player)
    {
      
    }

    public void HitRule(PlayerController player)
    {
        player.GetComponent<ParticlePlayer>().Play(1f);
        canMove[player].canMove = false;
        canMove[player].timeInStun = 1;
        if(point[player] > 3)
        {
            point[player] -= 3;
           
        }
        else
        {
            point[player] = 0;
        }
        player.playerUI.points.text = point[player].ToString();
        UpdateBasket(player);
    }

    public void Update()
    {
        if (!adicionolPoint)
        {
            timeOfGame -= Time.deltaTime;
            ShowTime();
            removePlayersInStun();
            IntanceObject();
            //goDownPlayers();
            if (timeOfGame <= 0)
            {
               
                WinRule();
            }

        }
    }

    void removePlayersInStun()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            if (!canMove[player].canMove)
            {
                canMove[player].timeInStun -= Time.deltaTime;
                if (canMove[player].timeInStun <= 0)
                    canMove[player].canMove = true;
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
        if (canMove[player.gameObject.GetComponent<PlayerController>()].canMove)
        {
            player.position += -dir * speed * Time.deltaTime;
        }
    }

    public void PointRule(PlayerController player)
    {
        point[player] += 1;
        player.playerUI.points.text = point[player].ToString();
        UpdateBasket(player);
    }
    public void UpdateBasket(PlayerController player)
    {
        if (point[player] > 0 && point[player] < 6 && player.gameObject.GetComponentInChildren<Basket>().type != 1)
        {
            GameObject.Destroy(player.gameObject.GetComponentInChildren<Basket>().gameObject);
            GameObject obj = GameObject.Instantiate(_basket2, new Vector3(player.transform.position.x, player.transform.position.y + 2.5f, player.transform.position.z), Quaternion.identity, player.transform).gameObject as GameObject;
            
            obj.GetComponent<Basket>().player = player;
            obj.GetComponent<Basket>().type = 1;
        }
        else if (point[player] > 6 && player.gameObject.GetComponentInChildren<Basket>().type != 2)
        {
            GameObject.Destroy(player.gameObject.GetComponentInChildren<Basket>().gameObject);
            GameObject obj = GameObject.Instantiate(_basket3, new Vector3(player.transform.position.x, player.transform.position.y + 2.5f, player.transform.position.z), Quaternion.identity, player.transform).gameObject as GameObject;
            obj.GetComponent<Basket>().player = player;
            obj.GetComponent<Basket>().type = 2;
        }
        else if(point[player] == 0 && player.gameObject.GetComponentInChildren<Basket>().type != 0)
        {
            GameObject.Destroy(player.gameObject.GetComponentInChildren<Basket>().gameObject);
            GameObject obj = GameObject.Instantiate(_basket1, new Vector3(player.transform.position.x, player.transform.position.y + 2.5f, player.transform.position.z), Quaternion.identity, player.transform).gameObject as GameObject;
            obj.GetComponent<Basket>().player = player;
            obj.GetComponent<Basket>().type = 0;
        }
      
    }
    public void RotationRule(Vector3 dir, Transform player)
    {
        
    }
    public void IntanceObject()
    {
        InstanceHiveTime -= Time.deltaTime;
        InstanceNutTime -= Time.deltaTime;
        if(InstanceNutTime <= 0)
        {
            GameObject aux  = GameObject.Instantiate(_Nut, new Vector3(Random.Range(-6.57f,4.83f),8.96f,Random.Range(-4.37f, 6.26f)), Quaternion.identity).gameObject as GameObject;
            InstanceNutTime = Random.Range(1f,3f);
        }
        if (InstanceHiveTime <= 0)
        {
            GameObject aux = GameObject.Instantiate(_Hive, new Vector3(Random.Range(-6.57f, 4.83f), 8.96f, Random.Range(-4.37f, 6.26f)), Quaternion.identity).gameObject as GameObject;
            InstanceHiveTime =Random.Range(2,4);
        }
    }
    public void StartGame()
    {
        AddPlayerInformations();
        InstanceHiveTime = Random.Range(2, 4);
        InstanceNutTime = 0.5f;
        GameController.singleton.uIManager.SumirTudo();
    }
    void AddPlayerInformations()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            point.Add(player, 0);
            player.playerUI.points.text = point[player].ToString();
            Stun auxStun = new Stun();
            auxStun.canMove = true;
            auxStun.timeInStun = 0;
            canMove.Add(player, auxStun);
            GameObject obj = GameObject.Instantiate(_basket1, new Vector3(player.transform.position.x, player.transform.position.y + 2.5f, player.transform.position.z),Quaternion.identity,player.transform).gameObject as GameObject;
            obj.GetComponent<Basket>().player = player;
            obj.GetComponent<Basket>().type = 0;
        }
    }
    public void WinRule()
    {
       
        PlayerController playerMaior = null;
        int maiorPonto = int.MinValue;
        foreach (PlayerController player in aux.playerManager.playersControllers)
        {
            GameObject.Destroy(player.gameObject.GetComponentInChildren<Basket>().gameObject);
            if (point[player] > maiorPonto)
            {
                maiorPonto = point[player];
                playerMaior = player;
            }
        }
        if (adicionolPoint == false)
        {
            GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(playerMaior)] += 1;
            aux.FinishGame();
            adicionolPoint = true;
        }
    }
}
