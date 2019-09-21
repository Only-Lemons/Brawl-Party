using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passivas",menuName = "PassivaEsqueleto")]
public class SOPassiveSkull : SOPassive
{

    bool isStart = false;
    public override void AtivarPassiva(PlayerController player)
    {
        if(isStart == false)
        {
            player.canDeath = false;
            isStart = true;
        }
        if(player.life <= 0 && CheckCD())
        {
            player.canDeath = true;
            player.life = Mathf.FloorToInt(player.player.hp * 0.30f);
       
        }
        else
        {
            player.canDeath = true;
        }
        
    }

    public override bool CheckCD()
    {
        if (inCD == true)
        {
            actualtimeCD -= Time.deltaTime;
            if (actualtimeCD <= 0)
            {
                actualtimeCD = timeCD;
                inCD = false;
                return true;
            }
            return false;
        }
        else
        {
            actualtimeCD = timeCD;
            return true;
        }
    }
}
