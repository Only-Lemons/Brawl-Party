
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRun : MiniGame
{   
    List<PlayerController> players = new List<PlayerController>();

    float timeOfGame;
    [SerializeField]GameObject _cross;
    [SerializeField]GhostController[] ghost;
    [SerializeField]GameObject _particula;
    
    Dictionary<GhostController, float> _canFollow = new Dictionary<GhostController, float>();
    Dictionary<PlayerController, bool> _isDead = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, int> _pointGeralForManager = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, int> _itemsforplayer = new Dictionary<PlayerController, int>();
    Dictionary<PlayerController, float> _timeinvenciblelayer = new Dictionary<PlayerController, float>();
    Dictionary<PlayerController, bool> _playerisinvencible = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, GameObject> _particlesInGame = new Dictionary<PlayerController, GameObject>();
    bool _adicionolPoint = false;
    float _tempoGame;
    float _timeToInstantiateNewCross;
    private void Start()
    {
        _timeToInstantiateNewCross = 1;
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

    }
    public void AddObjectInPlayer(PlayerController player)
    {
        _itemsforplayer[player] += 1;
        if(_itemsforplayer[player] == 3)
        {
           GameObject aux =  GameObject.Instantiate(_particula, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z), Quaternion.identity, player.transform).gameObject;
            _particlesInGame.Add(player, aux);
            _timeinvenciblelayer[player] = 2f;
            _itemsforplayer[player] = 0;
            _playerisinvencible[player] = true;
        }
    }
    void Update()
    {
         if (!_adicionolPoint)
        {
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
                GameObject gameObject =  GameObject.Instantiate(_cross,new Vector3(Random.Range(-7.58f, 8.75f), 1.8f,Random.Range(-5.22f, 8.57f)),Quaternion.identity).gameObject;
                gameObject.GetComponent<Cross>().modegame = this;
            }
            if (timeOfGame <= 0)
            {
                WinRule();
            }
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
                GameObject.Destroy(_particlesInGame[player]);
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
            _isDead[player] = true;
            player.gameObject.SetActive(false);
            if (VerifyPlayerMortos())
            {
                AddPointForPlayers();
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
        player.transform.position += player._movementAxis * player.speed * Time.deltaTime;
        if (player._movementAxis != Vector3.zero)
        {
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(player._movementAxis), Time.deltaTime * 20);
        }
    }

    public override void PointRule(PlayerController player)
    {
        playerPoints[player.gameObject.transform.parent.gameObject] += (int)(8 * _tempoGame);
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
        if (_adicionolPoint == false)
        {
            GameManager.Instance.WinMinigame();
            _adicionolPoint = true;
        }
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
        foreach (PlayerController player in players)
        {
            _playerisinvencible.Add(player, false);
            _itemsforplayer.Add(player, 0);
            _timeinvenciblelayer.Add(player, 0);
            playerPoints.Add(player.gameObject.transform.parent.gameObject, 0);
            //player.playerUI.points.text = pointPlayer[player].ToString();
            _isDead.Add(player, false);

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