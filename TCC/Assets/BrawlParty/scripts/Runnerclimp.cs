using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runnerclimp : MiniGame
{
    public List<PlayerController> players = new List<PlayerController>();

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
        throw new System.NotImplementedException();
    }

    public override void PointRule(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void RotationRule(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void WinRule()
    {
        throw new System.NotImplementedException();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
