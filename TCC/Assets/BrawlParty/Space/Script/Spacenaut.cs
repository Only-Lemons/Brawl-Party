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
            playerOxygen.Add(player, 0f);
            player.actualGameMode = this;
        }
    }

    private void Update()
    {
        
    }


    public override void Action(PlayerController player)
    {
        Collider[] colided = Physics.OverlapBox(player.transform.position, new Vector3(2, 2, 4), Quaternion.identity);
        float mDist = float.MaxValue;
        Collider mDistCollider = null;
        if(colided!= null)
        {
            for (int i = 0; i < colided.Length; i++)
            {
                //GAms pq o layermask nao quer funcionar
                if (colided[i].GetComponent<PlayerController>() != player && !colided[i].CompareTag("Planet"))
                {
                    float dist = Vector3.Distance(player.transform.position, colided[i].transform.position);
                    if (dist <= mDist)
                        mDistCollider = colided[i];
                }          
            }
            if(mDistCollider.CompareTag("Player"))
            {
                HitRule(mDistCollider.GetComponent<PlayerController>());
            }
            else if (mDistCollider.CompareTag("Oxygen"))
            {
                
                if(playerOxygen[player]<=100)
                    playerOxygen[player] += mDistCollider.GetComponent<Resource>().takeResource();
            }else if(mDistCollider.CompareTag("Nave"))
            {
                if (playerOxygen[player] > 0)
                {
                    playerOxygen[player] -= 10;
                    if(mDistCollider.GetComponent<Nave>().takeResource())
                    {
                        WinRule();
                    }
                }

                   

            }

        }
    }

    public override void HitRule(PlayerController player)
    {

        player.rb.AddForce(Vector3.forward* 10, ForceMode.Impulse);
    }

    public override void MovementRule(PlayerController player)
    {


       player.transform.position += (player._movementAxis) * player.speed * Time.fixedDeltaTime;
        
    }

    public override void PointRule(PlayerController player)
    {
        
    }

    public override void RotationRule(PlayerController player)
    {
        
        player.rb.MoveRotation(Quaternion.Euler(player._movementAxis));
    }

    public override void WinRule()
    {
        Debug.Log("ACABOOOOOOOOOOOOO");
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
        //Pular
        player.rb.AddForce(player.transform.up * 15f, ForceMode.Impulse);
    }
}
