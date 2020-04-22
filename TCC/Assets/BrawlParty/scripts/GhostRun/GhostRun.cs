using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRun : MiniGame
{   
    List<PlayerController> players = new List<PlayerController>();

    float timeOfGame;
    [SerializeField]GameObject _Monster;
    [SerializeField]GameObject _Ghost ;

    GhostController[] ghost;

    Dictionary<GhostController, float> canFollow = new Dictionary<GhostController, float>();
    Dictionary<PlayerController, bool> isGhost = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, int> _pointGeralForManager = new Dictionary<PlayerController, int>();
    List<PlayerController> winners = new List<PlayerController>();

    bool adicionolPoint = false;
    int numwinner = 0;
    int morreuAgoraMsm = 0;
    float tempoGame;
    private void Start()
    {
        timeOfGame = 30;
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        
        if(GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);

        foreach (var player in players)
        {
            player.actualGameMode = this;

        }
        AddPlayerInformations();
        InstantiateGhost();
        morreuAgoraMsm = players.Count-1;

    }

    void Update()
    {
         if (!adicionolPoint)
        {
            tempoGame += Time.deltaTime;
            timeOfGame -= Time.deltaTime;
            ShowTime();
            MoveGhost();
            AddPointForPlayers();
            if (timeOfGame <= 0)
            {

                WinRule();
            }
        }
    }

    public override void Action(PlayerController player)
    {
        throw new NotImplementedException();
    }

    public override void HitRule(PlayerController player)
    {
        isGhost[player] = true;
        morreuAgoraMsm--;
        player.morreuAgora += morreuAgoraMsm;
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.gameObject.GetComponent<Collider>().enabled = false;
        GameObject.Instantiate(_Monster, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity, player.transform);
        if (VerifyPlayerMortos())
        {
            AddPointForPlayers();
            WinRule();
        }
    }

    public override void Jump(PlayerController player)
    {
        throw new NotImplementedException();
    }

    public override void MovementRule(PlayerController player)
    {
        player.transform.position += player._movementAxis * player.speed * Time.deltaTime;
        if (player._movementAxis != Vector3.zero)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(player._movementAxis), Time.deltaTime * 20);
        }
    }

    public override void PointRule(PlayerController player)
    {
        playerPoints[player.gameObject.transform.parent.gameObject] += (int)(8 * tempoGame);
        Debug.Log(playerPoints[player.gameObject.transform.parent.gameObject] + " player: " + player.name);
        //player.playerUI.points.text = pointPlayer[player].ToString();
    }

    public override void RotationRule(PlayerController player)
    {
 // FAZ ND
    }

    public override void WinRule()
    {
        InsertWinners();
        foreach (PlayerController player in players)
        {
            GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += _pointGeralForManager[player];
        }
        if (adicionolPoint == false)
        {
            GameManager.Instance.WinMinigame();
            adicionolPoint = true;
        }
    }



    bool VerifyPlayerMortos()
    {
        int a = 0;
        for (int i = 0; i < isGhost.Count; i++)
        {
            if (isGhost[players[i]] == false)
                a++;
        }
        if (a > 1)
            return false;

        return true;
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
        foreach (PlayerController player in players)
        {
            playerPoints.Add(player.gameObject.transform.parent.gameObject, 0);
            //player.playerUI.points.text = pointPlayer[player].ToString();
            isGhost.Add(player, false);

        }
    }

    
    private void MoveGhost()
    {
        foreach (GhostController ghost in ghost) {
               
                PlayerController closerPlayer = players[0];
                float DistanciaMin = float.MaxValue;
                foreach (PlayerController player in players)
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
        for (int i = 0; i < players.Count; i++)
        {
            if (!isGhost[players[i]])
                PointRule(players[i]);
        }
    }

    void InsertWinners()
    {
        Dictionary<PlayerController, int> position = new Dictionary<PlayerController, int>();
        for (int i = 0; i < players.Count; i++)
        {
            position.Add(players[i], 3);
            for (int j = players.Count; j > i; j--)
            {
                if (playerPoints[players[i].gameObject.transform.parent.gameObject] < playerPoints[players[j % players.Count].gameObject.transform.parent.gameObject] && j != i)
                {
                    position[players[i]]--;
                    Debug.Log("Entrou aqui!!");
                }
                   
            }
            _pointGeralForManager.Add(players[i] ,position[players[i]]);
        }
    }

    public void ShowTime()
    {
        string minute = ((int)(timeOfGame / 60)).ToString("00"); ;
        string seconds = ((int)(timeOfGame % 60)).ToString("00"); ;
        //aux.time.text = minute + ":" + seconds;
    }
}