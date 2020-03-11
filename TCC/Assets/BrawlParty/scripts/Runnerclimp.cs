using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runnerclimp : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    public Camera camera;
    float timeUp;
    public float lowSpeed;

    public GameObject[] platform;
    public Transform[] posPlatform;
    float timePlatform;
    //public float distanceInstantiatePlatform;
    public float timeInstantiatePlatform;
    //GameObject tempPlatform;

    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        foreach (var player in players)
        {
            player.actualGameMode = this;
        }

        camera = Camera.main;
        timePlatform = 1;
        if (timeInstantiatePlatform <= 0)
            timeInstantiatePlatform = 2;

        //tempPlatform = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[Random.Range(0, posPlatform.Length)].position, Quaternion.identity);
    }

    public override void Action(PlayerController player)
    {
    }

    public override void HitRule(PlayerController player)
    {
    }

    public override void MovementRule(PlayerController player)
    {
        player.transform.position += player._movementAxis * player.speed * Time.fixedDeltaTime;
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
        timeUp += Acceleration(Time.deltaTime);
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + timeUp, camera.transform.position.z);

        

        PlatformGenerator();
    }
    float Acceleration(float t) //Aumenta dificuldade do jogo gradualmente
    {
        return (t * t) / lowSpeed;
    }

    void PlatformGenerator() //Comportamento e geração das plataformas
    {
        if (timeInstantiatePlatform > 0.2f)
        {
            timeInstantiatePlatform -= timeUp / 10;
        }
        else
            timeInstantiatePlatform = 0.2f;

        timePlatform += Time.deltaTime;
        if (timePlatform >= timeInstantiatePlatform)
        {
            GameObject temp = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[Random.Range(0, posPlatform.Length)].position, Quaternion.identity);
            timePlatform = 0;

        }
        //float distance = Vector3.Distance(tempPlatform.transform.position, posPlatform[1].position);
        //if (distance > distanceInstantiatePlatform)
        //{
        //    tempPlatform = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[Random.Range(0, posPlatform.Length)].position, Quaternion.identity);
        //}

    }

    // Update is called once per frame
    void Update()
    {
        SceneMechanics();
        WinRule();
    }

    void FixedUpdate()
    {
    }
}
