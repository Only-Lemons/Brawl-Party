using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkull : Passive
{
    public override void AtivarPassiva(PlayerController player)
    {
        if(player.life <= 0)
        {
            player.life = Mathf.FloorToInt(player.player.hp * 0.30f);
        }
    }
}
