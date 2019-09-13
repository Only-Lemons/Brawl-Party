using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passivas", menuName = "PassivaRobo")]
public class SOPassiveRobot : SOPassive
{
    Arma armaAtual;
 
    public override void AtivarPassiva(PlayerController player)
    {
        if(player.actualArma != null && CheckCD())
        {
            if (armaAtual != player.actualArma)
            {

                armaAtual = player.actualArma;
                player.AtivarEscudo(Mathf.FloorToInt(player.player.hp * 0.20f));
                inCD = true;
            }
           
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
