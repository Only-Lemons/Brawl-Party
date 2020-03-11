using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runnerclimp : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    public Camera camera;
    float timeUp;

    public GameObject[] platform;
    public Transform posSpawn;
    public Transform[] posPlatform;
    public float speed;

    //Stone
    public GameObject stonePrefab;
    public GameObject warningEffect;
    public float timeInstantiateStone;

    float randomStonePos;
    public float playerFallMultiplier;

    //Foot
    public GameObject footPlayer;

    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        foreach (var player in players)
        {
            player.actualGameMode = this;
            Instantiate(footPlayer, player.transform);
        }

        camera = Camera.main;
        if (speed <= 0)
            speed = 2;
        if (timeInstantiateStone <= 0)
            timeInstantiateStone = 4;

        InvokeRepeating("RollingStones", 10f, timeInstantiateStone);
        InvokeRepeating("instanciaPedra", 12f, timeInstantiateStone);
        InvokeRepeating("PlatformGenerator", 0, 2/speed);
    }

    public override void Action(PlayerController player)
    {
        player.rb.AddForce(Vector3.up * 10.5f, ForceMode.Impulse);
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

            if(player.rb.velocity.y < 3f)
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
        speed += Time.deltaTime / 100;
        timeUp = Time.deltaTime * speed;
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + timeUp, camera.transform.position.z);

        posSpawn.position = new Vector3(posSpawn.position.x, camera.transform.position.y + 8, posSpawn.position.z);
    }
    //float Acceleration(float t) //Aumenta dificuldade do jogo gradualmente
    //{
    //    //return (t * t);
    //    return t/200;
    //}

    void PlatformGenerator() //Comportamento e geração das plataformas
    {
        GameObject tempPlatform = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[Random.Range(0, posPlatform.Length)].position, Quaternion.identity);
    }

    void RollingStones()
    {
        print("RollingStones");
        randomStonePos = Random.Range(-6f, 6f);
        GameObject warning = Instantiate(warningEffect);
        warning.transform.position = new Vector3(randomStonePos, posSpawn.position.y - 2);
        warning.transform.parent = posSpawn.transform;
    }

    void instanciaPedra()
    {
        print("Instancia Pedra");
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
    }

    public override void Jump(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
