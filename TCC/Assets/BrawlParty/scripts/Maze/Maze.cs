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
    GameObject door;
    [SerializeField]
    GameObject FinishGame;
    [SerializeField]
    List<Jason> jasons = new List<Jason>();

    float fTime;
    float calculateLT = 1;

    public GameObject[] randomWall;
    public GameObject[] playersLight;
    public GameObject[] jasonsFear;
    public float timeWallRandomize = 5;
    float timeWR;

    private void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        randomWall = GameObject.FindGameObjectsWithTag("FakeWall");
        jasonsFear = GameObject.FindGameObjectsWithTag("Enemy");
        timeWR = timeWallRandomize;

        if (GameManager.Instance != null)
            GameManager.Instance.getPlayersMinigame(players);

        foreach (var player in players)
        {
            player.actualGameMode = this;
            inStun[player] = false;
            timeStun[player] = 0;
            lightPerPlayer[player] = 1;
        }

        //RandomWallInsert();

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
            foreach (var player in players)
            {
                float cpa = ((1 / Vector3.Distance(player.transform.position, jason.transform.position)) * lightPerPlayer[player] * (1 / Vector3.Distance(player.transform.position, FinishGame.transform.position)));
                if (cpa > coicidenteP && !inStun[player])
                {
                    playerF = player;
                    coicidenteP = cpa;
                }
            }
            print(playerF.name);
            jason.moviment(playerF.transform.position);
        }
    }
    private void Update()
    {
        ChoicePathJason();

        RandomWallInsert();
        FearLevel();
    }
    void ChangeLightPlayer()
    {
        calculateLT -= Time.fixedDeltaTime;
        if (calculateLT <= 0)
        {
            foreach (var player in players)
            {
                lightPerPlayer[player] = 0;
                foreach (var player2 in players)
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

        Vector3.Lerp(door.transform.position, new Vector3(door.transform.position.x + 4, door.transform.position.y, door.transform.position.z), 180);

    }
    public override void Action(PlayerController player)
    {

    }
    void RemoveStun()
    {
        foreach (var player in players)
        {
            if (inStun[player])
            {
                timeStun[player] -= Time.fixedDeltaTime;
                if (timeStun[player] <= 0)
                {
                    timeStun[player] = 0;
                    inStun[player] = false;
                }
            }
        }
    }

    void RandomWallInsert()
    {
        timeWallRandomize -= Time.fixedDeltaTime;
        if (timeWallRandomize <= 0)
        {
            foreach (GameObject g in randomWall)
            {
                int random = Random.Range(0, 2);
                g.SetActive(true);
                if (random == 0)
                {
                    g.SetActive(false);
                }
            }
            timeWallRandomize = timeWR;
        }
    }

    void FearLevel()
    {
        foreach(GameObject jason in jasonsFear)
        {
            foreach (GameObject player in playersLight)
            {
                if(Vector3.Distance(jason.gameObject.transform.position, player.transform.position) < 10)
                {
                    if (player.transform.localScale.x >= 3)
                        player.transform.localScale = Vector3.Lerp(player.transform.localScale, new Vector3(1, 1, 1), Time.fixedDeltaTime); ;
                }
                else
                {
                    player.transform.localScale = Vector3.Lerp(player.transform.localScale, new Vector3(15, 15, 1), Time.fixedDeltaTime);
                }
            }
        }
    }

    public override void HitRule(PlayerController player)
    {
        if (!inStun[player])
        {
            inStun[player] = true;
            timeStun[player] = 10;
        }
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
        players.Remove(player);
        switch (players.Count)
        {
            case 3:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 3;
                break;
            case 2:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 2;
                break;
            case 1:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 1;
                break;
            default:
                GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 0;
                break;
        }
        GameObject.Destroy(player.gameObject);
        WinRule();

    }

    public override void RotationRule(PlayerController player)
    {

    }

    public override void WinRule()
    {
        if (fTime <= 0 || players.Count == 0)
        {

            GameManager.Instance.WinMinigame();
        }
    }
}
