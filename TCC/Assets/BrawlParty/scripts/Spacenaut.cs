using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spacenaut : MiniGame
{
    public List<PlayerController> players= new List<PlayerController>();

    void Start()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());

        foreach (var player in players)
        {
            player.actualGameMode = this;
        }
    }
    
    public override void Action(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void HitRule(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void MovementRule(PlayerController player)
    {
       
        player.transform.position += player._movementAxis * player.speed * Time.fixedDeltaTime;
    }

    public override void PointRule(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void RotationRule(PlayerController player)
    {
        
    }

    public override void WinRule()
    {
        throw new System.NotImplementedException();
    }


}
