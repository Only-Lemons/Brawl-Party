﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GhostRun : MiniGame
{   
    List<PlayerController> players = new List<PlayerController>();

    float timeOfGame;
    [SerializeField]GameObject _cross;
    [SerializeField]GhostController[] ghost;
    [SerializeField]List<Images> _playersImages;
    [SerializeField] Sprite _morte;
    
    Dictionary<GhostController, float> _canFollow = new Dictionary<GhostController, float>();
    Dictionary<PlayerController, bool> _isDead = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, int> _pointGeralForManager = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, int> _itemsforplayer = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, float> _timeinvenciblelayer = new Dictionary<PlayerController, float>();
    Dictionary<PlayerController, bool> _playerisinvencible = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, int> _indexs = new Dictionary<PlayerController, int>();
    int _qtdVivo;
    float _tempoGame;
    float _timeToInstantiateNewCross;

    private void Start()
    {
        AudioController.Instance.PlayAudio("BGM", true);
        AudioController.Instance.PlayAudio("Ghost", true);

        _timeToInstantiateNewCross = 1;
        timeOfGame = 30;
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        
        if(GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);

        foreach (var player in players)
        {
            player.actualGameMode = this;

        }

        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if (i < players.Count)
            {
                players[i].setColor(GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor);
                players[i].playerIndiq.GetComponent<Renderer>().material.color = GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor * 4;
                InstanciarPlayer(players[i].transform, GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().selectPlayerObject);
            }
        }


        AddPlayerInformations();
        _qtdVivo = players.Count;
        InstantiateGhost();
        
    }
    public void AddObjectInPlayer(PlayerController player)
    {
        _itemsforplayer[player] += 1;
        _playersImages[_indexs[player]].quantidade.text = _itemsforplayer[player].ToString();
        if (_itemsforplayer[player] >= 3)
        {
            GameManager.Instance.particleManager.getParticula("invencibilidade", player.transform);
            _playersImages[_indexs[player]].quantidade.text = "0";
            _timeinvenciblelayer[player] = 2f;
            _itemsforplayer[player] = 0;
            _playerisinvencible[player] = true;
        }
    }
    void Update()
    {
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou() || GameManager.Instance.end)
            return;
        
            _tempoGame += Time.deltaTime;
            timeOfGame -= Time.deltaTime;
            ShowTime();
            MoveGhost();
            AddPointForPlayers();
            EfectInvenciblePlayers();
            _timeToInstantiateNewCross -= Time.deltaTime;
            if (_timeToInstantiateNewCross <= 0)
            {
                _timeToInstantiateNewCross = 2;
                GameObject gameObject =  GameObject.Instantiate(_cross,new Vector3(Random.Range(-7.58f, 8.75f), 1,Random.Range(-5.22f, 8.57f)),Quaternion.identity).gameObject;
                gameObject.GetComponent<Cross>().modegame = this;
            }
            if (timeOfGame <= 0)
            {
                TimeGameController.Instance.acabou = true;

            if(TimeGameController.Instance.AcabouMesmo())
                WinRule();
            }
   
    }
    void EfectInvenciblePlayers()
    {
        foreach (PlayerController player in players)
        {
            if (!_playerisinvencible[player])
                continue;
           
            _timeinvenciblelayer[player] -= Time.deltaTime;
            if (_timeinvenciblelayer[player] <= 0)
            {
                _playerisinvencible[player] = false;
            }
        }

    }

    public override void Action(PlayerController player)
    {
        //Fodas
    }

    public override void HitRule(PlayerController player)
    {
        if (!_playerisinvencible[player])
        {

            GameManager.Instance.particleManager.getParticula("morte", player.transform);
            _isDead[player] = true;
           
            player.gameObject.SetActive(false);
            PointRule(player);
            _qtdVivo--;
            AudioController.Instance.PlayAudio("Hit");
            _playersImages[_indexs[player]].fotoPersonagem.sprite = _morte;
            if (VerifyPlayerMortos())
            {
                WinRule();
            }
        }
    }

    public override void Jump(PlayerController player)
    {
       //FODAS
    }

    public override void MovementRule(PlayerController player)
    {
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou())
            return;
        player.transform.position += player._movementAxis * player.speed * Time.deltaTime;
        if (player._movementAxis != Vector3.zero)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(player._movementAxis), Time.deltaTime * 20);
        }
    }

    public override void PointRule(PlayerController player)
    {
        if (players.Count == 4)
        {
            switch (_qtdVivo)
            {
                case 1:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 3;
                    break;
                case 2:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 2;
                    break;
                case 3:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 1;
                    break;
                default:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 0;
                    break;
            }
        }
        else
        {
            switch (_qtdVivo)
            {

                case 1:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 1;
                    break;
                default:
                    GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 0;
                    break;
            }
        }

    }

    public override void RotationRule(PlayerController player)
    {
        // FAZ ND
    }

    public override void WinRule()
    {

       
       
            TimeGameController.Instance.acabou = true;
            GameManager.Instance.WinMinigame();

           
    }

    void WinScene()
    {
        
        if (TimeGameController.Instance.AcabouMesmo())
            GameManager.Instance.WinMinigame();
    }
   
    bool VerifyPlayerMortos()
    {
        int a = 0;
        for (int i = 0; i < _isDead.Count; i++)
        {
            if (_isDead[players[i]] == false)
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
            _canFollow.Add(newGhost, 0);
        }
    }

    void AddPlayerInformations()
    {
        int aux = 0;
        foreach (PlayerController player in players)
        {
            _playerisinvencible.Add(player, false);
            _itemsforplayer.Add(player, 0);
            _timeinvenciblelayer.Add(player, 0);
            playerPoints.Add(player.gameObject.transform.parent.gameObject, 0);
            //player.playerUI.points.text = pointPlayer[player].ToString();
            _isDead.Add(player, false);
            _playersImages[aux].quantidade.text = "0";
            _indexs.Add(player, aux);
           aux++;
        }
        for (int i = 0; i < _playersImages.Count; i++)
        {
            if( i < GameManager.Instance.playersPanels.Count)
                _playersImages[i].fotoPersonagem.sprite = GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().selectSprite;
            else
            {
                _playersImages[i].pai.SetActive(false);
            }
        }
 
    }

    
    private void MoveGhost()
    {
        foreach (GhostController ghost in ghost) {
               
                PlayerController closerPlayer = players[0];
                float DistanciaMin = float.MaxValue;
                foreach (PlayerController player in players)
                {
                    if (!_isDead[player] && DistanciaMin > Vector3.Distance(player.transform.position, ghost.transform.position))
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
            if (!_isDead[players[i]])
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
[System.Serializable]
public struct Images
{
    public GameObject pai;
    public Image fotoPersonagem;
    public Image fotoCruz;
    public TextMeshProUGUI  quantidade;
}