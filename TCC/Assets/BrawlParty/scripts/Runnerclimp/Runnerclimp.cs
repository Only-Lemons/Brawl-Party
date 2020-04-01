using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runnerclimp : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    public Camera camera;
    

    public GameObject[] platform;
    public Transform posSpawn;
    public Transform[] posPlatform;
    public float speed;
    public float distancePlatform;

    //Stone
    public GameObject stonePrefab;
    public GameObject warningEffect;
    public float timeInstantiateStone;
    public float playerFallMultiplier;

    //Foot
    public GameObject footPlayer;
    //

    float time;
    float timeUp;
    float platformPerSecond;
    float randomStonePos;

    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        foreach (var player in players)
        {
            player.actualGameMode = this;
            Instantiate(footPlayer, player.transform);
        }

        camera = Camera.main;

        InvokeRepeating("RollingStones", 10f, timeInstantiateStone);
        InvokeRepeating("WarningStone", 12f, timeInstantiateStone);
    }

    public override void Action(PlayerController player)
    {
        if (player.rb.velocity.y > -0.2f && player.rb.velocity.y < 0.2f)
            player.rb.AddForce(Vector3.up * (10f + (10f * timeUp)), ForceMode.Impulse);
    }

    public override void HitRule(PlayerController player)
    {
    }

    public override void MovementRule(PlayerController player)
    {
        player.transform.position += player._movementAxis * player.speed * Time.fixedDeltaTime;
    }

    void LockZ()
    {
        foreach (var player in players)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);

            if (player.rb.velocity.y < 3f)
            {
                player.rb.velocity += Vector3.up * Physics.gravity.y * (playerFallMultiplier - 1) * Time.deltaTime;
            }
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

    }

    void SceneMechanics() //Comportamento geral do cenário
    {
        speed += Time.deltaTime / 40;
        timeUp = Time.deltaTime * speed;
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + timeUp, camera.transform.position.z);

        posSpawn.position = new Vector3(posSpawn.position.x, camera.transform.position.y + 10.5f, posSpawn.position.z);
    }
    //float Acceleration(float t) //Aumenta dificuldade do jogo gradualmente
    //{
    //    //return (t * t);
    //    return t/200;
    //}

    void PlatformGenerator() //Comportamento e geração das plataformas
    {
        time += Time.deltaTime;
        platformPerSecond = speed / distancePlatform;
        if (time > 1 / (platformPerSecond))
        {
            int qtd = Random.Range(0, 5);
            if (qtd > 2)
            {
                int platRandom = Random.Range(0, posPlatform.Length);
                GameObject tempPlatform = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[platRandom].position, Quaternion.identity);
                int platNew1 = 0;
                do
                {
                    platNew1 = Random.Range(0, posPlatform.Length);
                }
                while (platRandom == platNew1);
                if (platNew1 != platRandom)
                {
                    GameObject tempPlatformMew1 = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[platNew1].position, Quaternion.identity);
                    int platNew2 = 0;
                    do
                    {
                        platNew2 = Random.Range(0, posPlatform.Length);
                    }
                    while (platNew2 == platNew1 || platNew2 == platRandom);
                    if (platNew2 != platRandom && platNew2 != platNew1)
                    {
                        GameObject tempPlatformMew2 = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[platNew2].position, Quaternion.identity);
                    }

                }
            }
            else if (qtd == 0)
            {
                int platRandom = Random.Range(0, posPlatform.Length);
                GameObject tempPlatform = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[platRandom].position, Quaternion.identity);
                int platNew1 = 0;
                do
                {
                    platNew1 = Random.Range(0, posPlatform.Length);
                }
                while (platRandom == platNew1);
                if (platNew1 != platRandom)
                {
                    GameObject tempPlatformMew1 = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[platNew1].position, Quaternion.identity);
                }
            }
            else
            {
                GameObject tempPlatform = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[Random.Range(0, posPlatform.Length)].position, Quaternion.identity);
            }
            time = 0;
        }

    }

    void WarningStone()
    {
        randomStonePos = Random.Range(-9f, 9f);
        GameObject warning = Instantiate(warningEffect);
        warning.transform.position = new Vector3(randomStonePos, posSpawn.position.y - 2);
        warning.transform.parent = posSpawn.transform;
    }

    void RollingStones()
    {
        GameObject stone = Instantiate(stonePrefab);
        stone.transform.position = new Vector3(randomStonePos, posSpawn.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //WinRule();
    }

    void FixedUpdate()
    {
        LockZ();
        SceneMechanics();
        PlatformGenerator();
    }

    public override void Jump(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
