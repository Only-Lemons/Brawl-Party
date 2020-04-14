using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Maze : MiniGame
{
    Dictionary<PlayerController, float> lightPerPlayer = new Dictionary<PlayerController, float>();
    List<PlayerController> players = new List<PlayerController>();
    Dictionary<PlayerController, bool> inStun = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, float> timeStun = new Dictionary<PlayerController, float>();
    [SerializeField]
    GameObject doors;
    [SerializeField]
    GameObject FinishGame;
    [SerializeField]
    List<Jason> jasons = new List<Jason>();

    [SerializeField]
    private Transform _doorFinalPosition = null;   

    float fTime;
    float calculateLT = 1;

    private void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        
        if(GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);

        foreach (var player in players)
        {
            player.actualGameMode = this;
            inStun[player] = false;
            timeStun[player] = 0;
            lightPerPlayer[player] = 1;
        }
        
        fTime = 5f;
    }

    private void FixedUpdate()
    {
     
        RemoveStun();
        CloseDoors();
        ChangeLightPlayer();
      
    }

    void ChoicePathJason()
    {
        foreach (var jason in jasons)
        {
            PlayerController playerF = players[0];
            float coicidenteP = 0;
            foreach(var player in players)
            {
                float cpa = ((1 / Vector3.Distance(player.transform.position, jason.transform.position)) * lightPerPlayer[player] * (1 / Vector3.Distance(player.transform.position, FinishGame.transform.position)));
                if ( cpa > coicidenteP && !inStun[player])
                {
                    playerF = player;
                    coicidenteP =cpa;
                }
            }
            print(playerF.name);
            jason.moviment(playerF.transform.position);
        }
    }
    private void Update()
    {
        ChoicePathJason();
    }
    void ChangeLightPlayer()
    {
        calculateLT -= Time.fixedDeltaTime;
        if(calculateLT <= 0)
        {
            foreach(var player in players)
            {
                lightPerPlayer[player] = 0;
                foreach(var player2 in players)
                {
                    if (Vector3.Distance(player.transform.position, player2.transform.position) < 2)
                        lightPerPlayer[player] += 0.10f;
                }
            }
            calculateLT = 1;
        }
    }
    void CloseDoors()
    {
        fTime -= Time.fixedDeltaTime;
        //doors[0].transform.position = Vector3.Lerp(doors[0].transform.position, new Vector3(-1.114f, doors[0].transform.position.y, doors[0].transform.position.z), 5);
        //doors[1].transform.position = Vector3.Lerp(doors[1].transform.position, new Vector3(1.36f, doors[1].transform.position.y, doors[1].transform.position.z), 5);

        Sequence seq = DOTween.Sequence();
        seq.Insert(0, doors.transform.DOLocalMoveX(_doorFinalPosition.localPosition.x + 4, 180f, false));

    }
    public override void Action(PlayerController player)
    {
        if (!inStun[player])
        {
            inStun[player] = true;
            timeStun[player] = 10;
        }
    }
    void RemoveStun()
    {
        foreach (var player in players)
        {
            if (inStun[player])
            {
                timeStun[player] -= Time.fixedDeltaTime;
                if(timeStun[player] <= 0)
                {
                    timeStun[player] = 0;
                    inStun[player] = false;
                }
            }
        }
    }

    public override void HitRule(PlayerController player)
    {
        players.Remove(player);
        switch (players.Count)
        {
            case 3:
                playerPoints[player] = 3;
                break;
            case 2:
                playerPoints[player] = 2;
                break;
            case 1:
                playerPoints[player] = 1;
                break;
            default:
                playerPoints[player] = 0;
                break;
        }

        WinRule();
        GameObject.Destroy(player.gameObject);
        
    }

    public override void Jump(PlayerController player)
    {
       //NÃO PULA
    }

    public override void MovementRule(PlayerController player)
    {
        if (!inStun[player])
        {
            player.transform.position += player._movementAxis * player.speed * Time.fixedDeltaTime;
        }
    }

    public override void PointRule(PlayerController player)
    {
       
    }

    public override void RotationRule(PlayerController player)
    {
       
    }

    public override void WinRule()
    {
       if(fTime <= 0 || players.Count == 0)
        {
            //GameManager.Instance.
        }
    }
}
