using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passivas",menuName = "PassivaEsqueleto")]
public class SOPassiveSkull : SOPassive
{
    public override void AtivarPassiva(PlayerController player)
    {
        if(player.life <= 0 && CheckCD())
        {
            player.life = Mathf.FloorToInt(player.player.hp * 0.30f);
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
