using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacenaut : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();
    public GameObject oxPrefab;
    Dictionary<PlayerController, float> playerOxygen = new Dictionary<PlayerController, float>();
    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        foreach (var player in players)
        {
            playerOxygen.Add(player, 100f);
            player.actualGameMode = this;
        }

        StartCoroutine(OxygenLoss());
        StartCoroutine(OxygenInstante());
    }

    private void Update()
    {
        
    }


    public override void Action(PlayerController player)
    {
        //Pular
        player.rb.AddForce(player.transform.up * 15f, ForceMode.Impulse);
    }

    public override void HitRule(PlayerController player)
    {
        //Quando ele acerta em algo
        playerOxygen[player] += 30f;
    }

    public override void MovementRule(PlayerController player)
    {
        //  player.rb.AddForce(Vector3.right * player._movementAxis.x * player.speed);

       // player.transform.position += (player._movementAxis).normalized * player.speed * Time.fixedDeltaTime;
        player.transform.Translate(player._movementAxis * player.speed * Time.fixedDeltaTime, Camera.main.transform);
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


    IEnumerator OxygenInstante()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            int rng = Random.Range(0, players.Count - 1);
            GameObject ox = Instantiate(oxPrefab, players[rng].transform.position + new Vector3(0,2,0),Quaternion.identity);
          

        }
    }


    public override void Jump(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
