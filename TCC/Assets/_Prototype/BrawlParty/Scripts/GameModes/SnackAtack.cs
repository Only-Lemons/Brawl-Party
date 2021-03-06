﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    float maiorPontuacao = 0;

    public SnackAtack(GameController controller, float time)
    {
        aux = controller;
        timeOfGame = time;
    }
    public void Action(PlayerController player)
    {
        if (canMove[player.gameObject.GetComponent<PlayerController>()].canMove)
        {
            player.ExplosaoPower();
        }
    }

    public void HitRule(PlayerController player)
    {
        player.GetComponent<ParticlePlayer>().Play(1f);
        canMove[player].canMove = false;
        canMove[player].timeInStun = 1;
        if (point[player] > 3)
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

    void AtualizarPosicao()
    {
        for (int i = 0; i < aux.spritePersonagens.Count; i++)
        {
            aux.posicoesPersonagens[i].value = Mathf.Lerp(aux.posicoesPersonagens[i].value, float.Parse(aux.playerManager.playersControllers[i].playerUI.points.text), Time.deltaTime);
            if (float.Parse(aux.playerManager.playersControllers[i].playerUI.points.text) > maiorPontuacao)
            {
                maiorPontuacao = Mathf.Lerp(maiorPontuacao, float.Parse(aux.playerManager.playersControllers[i].playerUI.points.text), Time.deltaTime * 2);
            }
            aux.posicoesPersonagens[i].maxValue = maiorPontuacao;

            if (aux.playerManager.playersControllers[i].explode)
                aux.powerSpritePersonagem[i].gameObject.SetActive(true);
            else
                aux.powerSpritePersonagem[i].gameObject.SetActive(false);
        }
    }

    bool setei = false;

    void SetarSpritesInGame()
    {
        if (!setei)
        {
            for (int i = 0; i < aux.spritePersonagens.Count; i++)
            {
                aux.posicoesPersonagens[i].transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = aux.spritePersonagens[i];
            }

            for (int i = 0; i < aux.posicoesPersonagens.Count; i++)
            {
                if (aux.posicoesPersonagens[i].transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite == null)
                    aux.posicoesPersonagens[i].gameObject.SetActive(false);
            }
            setei = true;
        }
    }

    public void Update()
    {
        AtualizarPosicao();
        SetarSpritesInGame();
        if (!adicionolPoint)
        {
            timeOfGame -= Time.deltaTime;
            ShowTime();
            removePlayersInStun();
            IntanceObject();
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
    public void MovementRule(PlayerController player)
    {
        if (canMove[player].canMove)
        {
            player.transform.position += -player._movementAxis * player.speed * Time.deltaTime;

            if (player._movementAxis != Vector3.zero)
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(-player._movementAxis), Time.deltaTime * 20);
            }
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
            GameObject obj = GameObject.Instantiate(_basket2, new Vector3(player.transform.position.x, player.transform.position.y + 2.1f, player.transform.position.z), Quaternion.identity, player.transform).gameObject as GameObject;

            obj.GetComponent<Basket>().player = player;
            obj.GetComponent<Basket>().type = 1;
            player.pontosGenericos = 1;
        }
        else if (point[player] >= 6 && player.gameObject.GetComponentInChildren<Basket>().type != 2)
        {
            GameObject.Destroy(player.gameObject.GetComponentInChildren<Basket>().gameObject);
            GameObject obj = GameObject.Instantiate(_basket3, new Vector3(player.transform.position.x, player.transform.position.y + 2.1f, player.transform.position.z), Quaternion.identity, player.transform).gameObject as GameObject;
            obj.GetComponent<Basket>().player = player;
            obj.GetComponent<Basket>().type = 2;
            player.pontosGenericos = 2;
        }
        else if (point[player] == 0 && player.gameObject.GetComponentInChildren<Basket>().type != 0)
        {
            GameObject.Destroy(player.gameObject.GetComponentInChildren<Basket>().gameObject);
            GameObject obj = GameObject.Instantiate(_basket1, new Vector3(player.transform.position.x, player.transform.position.y + 2.1f, player.transform.position.z), Quaternion.identity, player.transform).gameObject as GameObject;
            obj.GetComponent<Basket>().player = player;
            obj.GetComponent<Basket>().type = 0;
            player.pontosGenericos = 0;
        }

    }
    public void RotationRule(PlayerController player)
    {

    }
    public void IntanceObject()
    {
        InstanceHiveTime -= Time.deltaTime;
        InstanceNutTime -= Time.deltaTime;
        if (InstanceNutTime <= 0)
        {
            for (int i = 0; i < aux.playerManager.playersControllers.Count - 1; i++)
            {
                GameObject aux = GameObject.Instantiate(_Nut, new Vector3(Random.Range(-6.57f, 4.83f), 8.96f, Random.Range(-4.37f, 6.26f)), Quaternion.identity).gameObject as GameObject;
            }

            InstanceNutTime = Random.Range(2f, 4f);
        }
        if (InstanceHiveTime <= 0)
        {
            GameObject aux = GameObject.Instantiate(_Hive, new Vector3(Random.Range(-6.57f, 4.83f), 8.96f, Random.Range(-4.37f, 6.26f)), Quaternion.identity).gameObject as GameObject;
            InstanceHiveTime = Random.Range(2, 4);
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
            GameObject obj = GameObject.Instantiate(_basket1, new Vector3(player.transform.position.x, player.transform.position.y + 2.1f, player.transform.position.z), Quaternion.identity, player.transform).gameObject as GameObject;
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
            for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
            {
                GameManager.Instance.pontosGeral[i] += aux.playerManager.playersControllers[i].pontosGenericos;
            }

            GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(playerMaior)] += 1;
            aux.FinishGame();
            adicionolPoint = true;
        }
    }
}
