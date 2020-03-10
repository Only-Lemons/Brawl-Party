using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runnerclimp : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    public Camera camera;
    public float timeUp;
    public int speed;

    public GameObject[] platform;
    public Transform[] posPlatform;
    float timePlatform;
    public float timeInstantiatePlatform;


    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        foreach (var player in players)
        {
            player.actualGameMode = this;
        }

        camera = Camera.main;
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
        timeUp += Time.fixedDeltaTime / speed;
        if(timeInstantiatePlatform > 0.3f)
            timeInstantiatePlatform -= timeUp / 2000;
        float time = Time.fixedDeltaTime * Acceleration(timeUp);
        camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + time, camera.transform.position.z);

        PlatformGenerator();
    }
    float Acceleration(float t) //Aumenta dificuldade do jogo gradualmente
    {
        return Mathf.Exp(t);
    }

    void PlatformGenerator() //Comportamento e geração das plataformas
    {
        timePlatform += Time.deltaTime;
        if(timePlatform > timeInstantiatePlatform)
        {
            GameObject plat = Instantiate(platform[Random.Range(0, platform.Length)], posPlatform[Random.Range(0, posPlatform.Length)].position, Quaternion.identity);

            timePlatform = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        SceneMechanics();
        WinRule();
    }
}
