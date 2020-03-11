using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacenaut : MiniGame
{
    public List<PlayerController> players= new List<PlayerController>();
    Dictionary<PlayerController, float> playerOxygen = new Dictionary<PlayerController, float>();


    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        foreach (var player in players)
        {
            player.actualGameMode = this;
            playerOxygen.Add(player, 100f);
        }


        StartCoroutine(OxygenLoss());

    }

    private void Update()
    {
        
    }


    public override void Action(PlayerController player)
    {
        //Pular
        player.rb.AddForce(Vector3.up * 15f,ForceMode.Impulse);
    }

    public override void HitRule(PlayerController player)
    {
        //Quando ele acerta em algo
        playerOxygen[player] += 30f;
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
        throw new System.NotImplementedException();
    }


    void Death(PlayerController player)
    {
        //playerOxygen.Remove(player);
        players.Remove(player);
        Destroy(player.gameObject);
    }

    IEnumerator OxygenLoss()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            foreach (var player in players)
            {
                playerOxygen[player] -= 5f;
                if (playerOxygen[player] <= 0f)
                {
                    Debug.Log("MORRI");
                    Death(player);
                }
            }     
        }
    }


}
