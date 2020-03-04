using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passivas",menuName = "Passivas/PassivaEsqueleto")]
public class SOPassiveSkull : SOPassive
{
    bool _isStart = false;

    public override void AtivarPassiva(PlayerController player)
    {
        if(_isStart == false)
        {
            player.canDeath = false;
            _isStart = true;
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
