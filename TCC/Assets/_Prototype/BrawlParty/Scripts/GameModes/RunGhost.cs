using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunGhost : IGameMode
{
    GameController aux;
    float timeOfGame;
    GameObject _Monster = Resources.Load("Mecanicas/Monster") as GameObject;
    GameObject _Ghost = Resources.Load("Mecanicas/Ghost") as GameObject;
    GhostController[] ghost;
    Dictionary<PlayerController, int> pointPlayer = new Dictionary<PlayerController, int>();
    Dictionary<GhostController, float> canFollow = new Dictionary<GhostController, float>();
    Dictionary<PlayerController, bool> isGhost = new Dictionary<PlayerController, bool>();
    List<PlayerController> winners = new List<PlayerController>();
    bool adicionolPoint = false;
    int numwinner = 0;
    int morreuAgoraMsm = 0;
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
        if (a > 1)
            return false;

        return true;
    }
    public void Action(PlayerController player)
    {

    }

    public void HitRule(PlayerController player)
    {
        isGhost[player] = true;
        morreuAgoraMsm--;
        player.morreuAgora += morreuAgoraMsm;
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.gameObject.GetComponent<Collider>().enabled = false;
        GameObject.Instantiate(_Monster, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity, player.transform);
        if (VerifyPlayerMortos())
            WinRule();
    }

    public void MovementRule(PlayerController player)
    {
        player.transform.position += player._movementAxis * player.speed * Time.deltaTime;
        if (player._movementAxis != Vector3.zero)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(player._movementAxis), Time.deltaTime * 20);
        }
    }

    public void PointRule(PlayerController player)
    {
        pointPlayer[player] += (int)(8 * Time.deltaTime);
        player.playerUI.points.text = pointPlayer[player].ToString();
    }

    public void RotationRule(PlayerController player)
    {

        //player.rotation = Quaternion.Lerp(player.rotation,Quaternion.Euler(dir.x,dir.y,dir.z), Time.deltaTime);
    }

    public void StartGame()
    {
        AddPlayerInformations();
        InstantiateGhost();
        GameController.singleton.uIManager.SumirTudo();
        morreuAgoraMsm = aux.playerManager.playersControllers.Count-1;
    }

    private void InstantiateGhost()
    {
        ghost = GameObject.FindObjectsOfType<GhostController>();
        foreach(GhostController newGhost in ghost)
        {
            canFollow.Add(newGhost, 0);
        }
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
        foreach (GhostController ghost in ghost) {
               
                PlayerController closerPlayer = aux.playerManager.playersControllers[0];
                float DistanciaMin = float.MaxValue;
                foreach (PlayerController player in aux.playerManager.playersControllers)
                {
                    if (!isGhost[player] && DistanciaMin > Vector3.Distance(player.transform.position, ghost.transform.position))
                    {
                        closerPlayer = player;
                        DistanciaMin = Vector3.Distance(player.transform.position, ghost.transform.position);
                    }
                }
                ghost.FollowPlayer(closerPlayer);
        }
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
        foreach (PlayerController player in aux.playerManager.playersControllers)
        {
            if (isGhost[player] == false)
            {
                GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(player)] += aux.playerManager.playersControllers.Count - 1;
            }
            else
            {
                GameManager.Instance.pontosGeral[aux.playerManager.playersControllers.IndexOf(player)] += (aux.playerManager.playersControllers.Count-2) - player.morreuAgora;
            }
        }
        if (adicionolPoint == false)
        {
            aux.FinishGame();
            adicionolPoint = true;
        }
    }
}