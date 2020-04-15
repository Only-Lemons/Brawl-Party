using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBirds : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    public GameObject[] obstaculos;
    public float timeForInstantiate = 2;
    public float velocidade;
    float velocidadeDificult;
    float timeFI;
    public Vector3 posInstantiate;

    void Start()
    {
        timeFI = timeForInstantiate;

        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        foreach (var player in players)
        {
            player.actualGameMode = this;
        }
    }

    

    public override void Action(PlayerController player)
    {
        player.rb.velocity = Vector3.zero;
        player.rb.AddForce(Vector3.up * 7f, ForceMode.Impulse);
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
            if (player.transform.position.x <= -10)
                player.transform.position = new Vector3(-10, player.transform.position.y, 0);
            if (player.transform.position.x >= 10)
                player.transform.position = new Vector3(-10, player.transform.position.y, 0);
            if (player.transform.position.y <= -6)
                player.transform.position = new Vector3(player.transform.position.x, -6, 0);
            if (player.transform.position.y >= 6)
                player.transform.position = new Vector3(player.transform.position.x, 6, 0);
            
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

    }

    void ObstaculoGenerator() //Comportamento e geração dos obstaculos
    {
        timeForInstantiate -= Time.deltaTime;
        if(timeForInstantiate <= 0)
        {
            GameObject x = Instantiate(InstanciarObstaculo(), posInstantiate, Quaternion.identity, this.transform);
            x.transform.position = RandomPosYObstaculo(x, 5);
            x.GetComponent<ObstaculoFB>().gravidade = new Vector3(-1.0f - velocidadeDificult, 0.0f, 0.0f);
            Destroy(x, 10);

            velocidadeDificult += velocidade;
            timeForInstantiate = timeFI;
        }
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
        ObstaculoGenerator();
    }

    public override void Jump(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    GameObject InstanciarObstaculo()
    {
        return obstaculos[Random.Range(0, obstaculos.Length)];
    }

    Vector3 RandomPosYObstaculo(GameObject obj,float minMax)
    {
        return new Vector3(obj.transform.position.x, Random.Range(-minMax, minMax), obj.transform.position.z);
    }
}
