﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGhost : IGameMode
{
    GameController aux;
    float timeOfGame;
    GameObject _Monster = Resources.Load("Mecanicas/Monster") as GameObject;
    GameObject _Ghost = Resources.Load("Mecanicas/Ghost") as GameObject;
    GhostController ghost;
    Dictionary<PlayerController, int> pointPlayer = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, bool> isGhost = new Dictionary<PlayerController, bool>();
    List<PlayerController> winners = new List<PlayerController>();
    bool adicionolPoint = false;
    int numwinner = 0;
    public RunGhost(GameController gameController, float time)
    {
        aux = gameController;
        timeOfGame = time;
    }
    bool VerifyPlayerMortos()
    {
        int a = 0;
        for (int i = 0; i < isGhost.Count; i++)
        {
            if (isGhost[GameController.singleton.playerManager.playersControllers[i]] == false)
                a++;
        }
        if (a >= 1)
            return false;

        return true;
    }
    public void Action(PlayerController player)
    {

    }

    public void HitRule(PlayerController player)
    {
        isGhost[player] = true;
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.gameObject.GetComponent<Collider>().enabled = false;
        GameObject.Instantiate(_Monster, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity, player.transform);
        if (VerifyPlayerMortos())
            WinRule();
    }

    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        player.position += dir * speed * Time.deltaTime;
    }

    public void PointRule(PlayerController player)
    {
        pointPlayer[player] += (int)(8 * Time.deltaTime);
        player.playerUI.points.text = pointPlayer[player].ToString();
    }

    public void RotationRule(Vector3 dir, Transform player)
    {

    }

    public void StartGame()
    {
        AddPlayerInformations();
        InstantiateGhost();
    }

    private void InstantiateGhost()
    {
        ghost = GameObject.FindObjectOfType<GhostController>();
    }

    void AddPlayerInformations()
    {
        foreach (PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            pointPlayer.Add(player, 0);
            player.playerUI.points.text = pointPlayer[player].ToString();
            isGhost.Add(player, false);

        }
    }
    public void Update()
    {
        if (!adicionolPoint)
        {
            timeOfGame -= Time.deltaTime;
            ShowTime();
            MoveGhost();
            AddPointForPlayers();
            if (timeOfGame <= 0)
            {

                InsertWinners();
                WinRule();
            }
        }
    }

    private void MoveGhost()
    {
        PlayerController closerPlayer = aux.playerManager.playersControllers[0];
        float DistanciaMin = float.MinValue;
        foreach (PlayerController player in aux.playerManager.playersControllers)
        {
            if (!isGhost[player] && DistanciaMin < Vector3.Distance(player.transform.position, ghost.transform.position))
            {
                closerPlayer = player;
                DistanciaMin = Vector3.Distance(player.transform.position, ghost.transform.position);
            }
        }
        ghost.FollowPlayer(closerPlayer);
    }

    private void AddPointForPlayers()
    {
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if (!isGhost[aux.playerManager.playersControllers[i]])
                PointRule(aux.playerManager.playersControllers[i]);
        }
    }

    void InsertWinners()
    {
        for (int i = 0; i < aux.playerManager.playersControllers.Count; i++)
        {
            if (!isGhost[aux.playerManager.playersControllers[i]])
            {
                winners.Add(aux.playerManager.playersControllers[i]);
            }
        }
    }
    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00"); ;
        string seconds = ((int)(timeOfGame % 60)).ToString("00"); ;
        aux.time.text = minute + ":" + seconds;
    }
    public void WinRule()
    {
        PlayerController playerMaior = null;
        int maiorPonto = int.MinValue;
        foreach (PlayerController player in aux.playerManager.playersControllers)
        {
            player.transform.GetChild(1).gameObject.SetActive(true);
            player.gameObject.GetComponent<Collider>().enabled = true;
            GameObject.Destroy(player.gameObject.GetComponentInChildren<Ghost>().gameObject);
            if (pointPlayer[player] > maiorPonto)
            {
                maiorPonto = pointPlayer[player];
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