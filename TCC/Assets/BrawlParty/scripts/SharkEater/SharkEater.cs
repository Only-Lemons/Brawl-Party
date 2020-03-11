using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkEater : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    [SerializeField]
    List<Shark> sharks = new List<Shark>();
    [SerializeField]
    List<GameObject> fences = new List<GameObject>();
    Dictionary<PlayerController, float> blooding = new Dictionary<PlayerController, float>();
    Dictionary<PlayerController, float> timeInBlooding = new Dictionary<PlayerController, float>();
    // PODE SER NO MINIGAME

    float timeToEat = 0;
    bool playerWin;

    void Start()
    {
      
        playerWin = false;
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        foreach (var player in players)
        {
            player.actualGameMode = this;
            blooding[player] = 0;
            timeInBlooding[player] = 0;
        }
    }
    void stopBlooding()
    {
        foreach (var player in players)
        {
            if(timeInBlooding[player] > 0)
            {
                timeInBlooding[player] -= Time.deltaTime;
                if (timeInBlooding[player] <= 0) {
                    timeInBlooding[player] = 0;
                    blooding[player] = 0;
                }
            }
        }
    }
    public override void Action(PlayerController player)
    {
        blooding[player] += 0.1f;
        timeInBlooding[player] += 3f;
    }
    public override void HitRule(PlayerController player)
    {
        players.Remove(player);
        switch (players.Count) {
            case 0:
                playerPoints[player] = 3;
                break;
            case 1:
                playerPoints[player] = 2;
                break;
            case 2:
                playerPoints[player] = 1;
                break;

        }
        
        WinRule();
        if (!playerWin) {
            GameObject.Destroy(player.gameObject);
        }
    }

    public override void MovementRule(PlayerController player)
    {
        // COPIAR DO PEDRO  
        player.transform.position += player._movementAxis * player.speed * Time.fixedDeltaTime;
    }

    public override void PointRule(PlayerController player)
    {
        
    }

    public override void RotationRule(PlayerController player)
    {
        // COPIAR DO PEDRO
    }

    public override void WinRule()
    {
        if (players.Count < 2)
        {
            playerWin = true;
            //empate


        } 
    }
    Vector3 getNearestFence(PlayerController player)
    {
        Vector3 aux = fences[0].transform.position;
        float minDistance = Vector3.Distance(player.transform.position, fences[0].transform.position);
        foreach (var fence in fences)
        {
            if(Vector3.Distance(player.transform.position,fence.transform.position)< minDistance)
            {
                aux = fence.transform.position;
                minDistance = Vector3.Distance(player.transform.position, fence.transform.position);
            }
        }
        return aux;
    }
    void sharkController()
    {
        timeToEat += Time.deltaTime;
        if (timeToEat > 1)
        {
            foreach (var shark in sharks)
            {
                Vector3 aux = Vector3.zero;
                foreach (var player in players)
                {
                    if(Random.Range(0f,1f) < blooding[player])
                    {
                        aux = getNearestFence(player);
                    }
                }
                if (aux == Vector3.zero) {
                    int numCer = Random.Range(0, fences.Count);
                    aux = fences[numCer].transform.position;
                }
                shark.Moviment(aux);
                if(timeToEat >= 3)
                {
                    timeToEat = 0;
                    shark.Jump();
                }

            }
        }
    }
    void Update()
    {
        sharkController();
        stopBlooding();
    }

    public override void Jump(PlayerController player)
    {
        player.rb.AddForce(Vector3.up * 15f, ForceMode.Impulse);
    }
}
