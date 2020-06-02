using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Reflection.Emit;

public class Maze : MiniGame
{

    [SerializeField]
    List<GraphsGhostDistance> _DistancesFuzzy;
   
   

    Dictionary<PlayerController, float> lightPerPlayer = new Dictionary<PlayerController, float>();
    List<PlayerController> players = new List<PlayerController>();
    Dictionary<PlayerController, bool> inStun = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, bool> withKey = new Dictionary<PlayerController, bool>();
    Dictionary<PlayerController, float> timeStun = new Dictionary<PlayerController, float>();
    [SerializeField]
    GameObject door;
    [SerializeField]
    GameObject FinishGame;
    [SerializeField]
    List<Jason> jasons = new List<Jason>();

    Vector3 closedDoor;

    float fTime;
    float calculateLT = 1;

    public GameObject exitLight;
    public GameObject[] randomWall;
    public GameObject[] playersLight;
    public GameObject[] jasonsFear;

    public GameObject keyExit;
    GameObject keyExists;

    public GameObject[] keysSpawn;
    public float timeWallRandomize = 5;
    float timeWR;
    int friend;

    bool isFinish = false;

    private void Start()
    {
        closedDoor = new Vector3(15, door.transform.position.y, door.transform.position.z);
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
            withKey[player] = false;
            timeStun[player] = 0;
            lightPerPlayer[player] = 1;
        }

        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if (i < players.Count)
            {
                players[i].setColor(GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor);
                players[i].playerIndiq.GetComponent<Renderer>().material.color =  GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor * 4;
           
            }
        }

        //RandomWallInsert();

        fTime = 120f;

        KeysSpawn();

        AudioController.Instance.PlayAudio("BGM", true);
    }

    private void FixedUpdate()
    {
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou() || GameManager.Instance.end)
            return;
        RemoveStun();
        CloseDoors();
        ChangeLightPlayer();
        UnbuggPlayerNonKey();
        PlayerStuned();
        WinRule();
    }

    void ChoicePathJason()
    {
        foreach (var jason in jasons)
        {
            PlayerController playerF = players[0];
            float coicidenteP = 0;
            int weightKey = 1;
            foreach (var player in players)
            {
                if (!inStun[player])
                    continue;
                if (withKey[player])
                    weightKey = 3;
                float cpa = (getWeightDistanceJasonPlayer(Vector3.Distance(player.transform.position, jason.transform.position)) * lightPerPlayer[player] * (getWeightDistancePlayerDoor(Vector3.Distance(player.transform.position, FinishGame.transform.position))))* weightKey;
              

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
    int getWeightDistanceJasonPlayer(float time) // A minima distancia é 2 e a máxima 50 fazer gráfico pensando nisso sendo o ultimo time 40 e o primeiro 4
    {
        float perto = _DistancesFuzzy[0].Perto.Evaluate(time);
        float medio = _DistancesFuzzy[0].Medio.Evaluate(time);
        float longe = _DistancesFuzzy[0].Longe.Evaluate(time);
        int peso = 2;
        if (perto < medio && longe < medio)
            peso = 1;
        if (medio < longe && perto < longe)
            peso = 0;
        return peso;
    }
    int getWeightDistancePlayerDoor(float time)// A minima distancia é 4 e a máxima 40 fazer gráfico pensando nisso sendo o ultimo time 40 e o primeiro 4
    {
        float perto = _DistancesFuzzy[1].Perto.Evaluate(time);
        float medio = _DistancesFuzzy[1].Medio.Evaluate(time);
        float longe = _DistancesFuzzy[1].Longe.Evaluate(time);
        int peso = 0;
        if (perto < medio && longe < medio)
            peso = 2;
        if (medio < longe && perto < longe)
            peso = 1;
        return peso;
        
    }
    private void Update()
    {
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou())
            return;
        ChoicePathJason();
        RandomWallInsert();
        //FearLevel();
        FriendlyLevel();
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

        door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(closedDoor.x, door.transform.position.y, door.transform.position.z), Time.fixedDeltaTime / (fTime));
        exitLight.transform.localScale = Vector3.Lerp(exitLight.transform.localScale, new Vector3(0, 1, 0), Time.fixedDeltaTime / (fTime));

    }
    public override void Action(PlayerController player)
    {
        Debug.Log("KEY");
        {
            if (Vector3.Distance(player.gameObject.transform.position, keyExists.gameObject.transform.position) < 2.36f)
            {
                KeyPlayer(player);
                AudioController.Instance.PlayAudio("Coleta");
            }
        }
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
        foreach (GameObject jason in jasonsFear)
        {
            foreach (GameObject player in playersLight)
            {
                if (Vector3.Distance(jason.transform.position, player.transform.position) < 10)
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

    void FriendlyLevel()
    {
        foreach (GameObject player in playersLight)
        {
            foreach (GameObject player2 in playersLight)
            {
                if (Vector3.Distance(player.transform.position, player2.transform.position) < 10)
                {
                    if (player.transform.position != player2.transform.position)
                        if (friend < 5)
                            friend++;
                    if (player.transform.localScale.x <= 35)
                        player.transform.localScale = Vector3.Lerp(player.transform.localScale, new Vector3(friend * 15, friend * 15, 1), Time.fixedDeltaTime); ;
                }
                else
                {
                    player.transform.localScale = Vector3.Lerp(player.transform.localScale, new Vector3(15, 15, 1), Time.fixedDeltaTime);
                    if (friend > 0)
                        friend--;
                }
            }
        }
    }

    public override void HitRule(PlayerController player)
    {
        if (!inStun[player])
        {
            inStun[player] = true;
            timeStun[player] = 2;

            AudioController.Instance.PlayAudio("Hit");
        }
    }

    public override void Jump(PlayerController player)
    {
        //NÃO PULA
    }

    public override void MovementRule(PlayerController player)
    {
        if (!TimeGameController.Instance.Comecou() && !TimeGameController.Instance.Acabou())
            return;
        if (!inStun[player])
        {
            player.transform.position += player._movementAxis * player.speed * Time.fixedDeltaTime;
            if (player._movementAxis != Vector3.zero)
            {
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(player._movementAxis), Time.deltaTime * 20);
            }
        }
    }

    public override void PointRule(PlayerController player)
    {
        if (withKey[player])
        {
            AudioController.Instance.PlayAudio("PlayWin");
            //players.Remove(player);
            //switch (players.Count)
            //{
            //    case 3:
            //        GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 3;
            //        break;
            //    case 2:
            //        GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 2;
            //        break;
            //    case 1:
            //        GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 1;
            //        break;
            //    default:
            //        GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += 0;
            //        break;
            //}
            GameManager.Instance.playersPontos[player.gameObject.transform.parent.gameObject] += players.Count -1;

            players.Remove(player);
            GameObject.Destroy(player.gameObject);

            if (players.Count <= 1)
                isFinish = true;
            else
                KeysSpawn();

            
        }
    }

    public override void RotationRule(PlayerController player)
    {

    }

    public override void WinRule()
    {
        if (fTime <= 3 || isFinish)
        {
            TimeGameController.Instance.acabou = true;
            if(TimeGameController.Instance.AcabouMesmo())
                GameManager.Instance.WinMinigame();
        }
    }

    void KeysSpawn()
    {
        AudioController.Instance.PlayAudio("KeySpawn");

        int pos = Random.Range(0, keysSpawn.Length);
        keyExists = Instantiate(keyExit);
        while (keysSpawn[pos] == null)
            pos = Random.Range(0, keysSpawn.Length);

        keyExists.transform.position = keysSpawn[pos].transform.position;
        keysSpawn[pos] = null;
    }

    public void KeyPlayer(PlayerController player)
    {
        if (!withKey[player])
        {
            //remove todas as chaves
            foreach (var p in players)
            {
                if (withKey[p])
                {
                    withKey[p] = false;
                    HitRule(p);
                }
            }
            //novo jogador com a chave
            withKey[player] = true;
            if (keyExists != null)
                Destroy(keyExists);
            keyExists = Instantiate(keyExit, player.transform.position + new Vector3(0, 2, 0), Quaternion.identity, player.gameObject.transform);

            AudioController.Instance.PlayAudio("Hit");
        }

    }
    void UnbuggPlayerNonKey()
    {
        foreach (PlayerController p in players)
        {
            if (!withKey[p])
                if (p.transform.position.z >= 21)
                    p.transform.position = new Vector3(p.transform.position.x, p.transform.position.y, 21);
        }
    }

    void PlayerStuned()
    {
        foreach (var player in players)
        {
            if (inStun[player])
            {
                if (timeStun[player] >= 0)
                {
                    player.transform.Rotate(0, 360 * Time.fixedDeltaTime * 3, 0);
                }
            }
        }
    }
}
[System.Serializable]
struct GraphsGhostDistance
{
   public string name;
   public AnimationCurve Perto;
   public AnimationCurve Medio;
   public AnimationCurve Longe;
}
