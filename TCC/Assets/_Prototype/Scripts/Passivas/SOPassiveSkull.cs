using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="Passiva Esqueleto",menuName ="Passivas")]
public class SOPassiveSkull : SOPassive
{
    public override void AtivarPassiva(PlayerController player)
    {
        if(player.life <= 0)
        {
            player.life = Mathf.FloorToInt(player.player.hp * 0.30f);
        }
    }
}
