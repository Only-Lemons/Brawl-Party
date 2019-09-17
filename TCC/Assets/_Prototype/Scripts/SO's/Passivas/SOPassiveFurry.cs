using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passivas", menuName = "PassivaFurry")]
public class SOPassiveFurry : SOPassive
{
    Arma armaAtual;
    bool isActive = false;
    float time = 2f;
    public override void AtivarPassiva(PlayerController player)
    {
        if (Cronometro())
            FinishPassive(player);
        if (player.actualArma != null && isActive == false && CheckCD())
        {
            if (armaAtual != player.actualArma)
            {
                armaAtual = player.actualArma;
                player.speed += player.player.speed/2;
                isActive = true;
               
            }

        }
        if(player.PowerUp && isActive == false && CheckCD())
        {
            armaAtual = player.actualArma;
            player.speed += player.player.speed/2;
            isActive = true;
            
        }

    }
    void FinishPassive(PlayerController player)
    {
        isActive = false;
        player.speed -= player.player.speed/2;
        inCD = true;
    }
    bool Cronometro()
    {
        if (isActive == true)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 2f;
                return true;
            }            
              
            
        }
        return false;
    }
    public override bool CheckCD()
    {
        if(inCD == true)
        {
            actualtimeCD -= Time.deltaTime;
            if(actualtimeCD <= 0)
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
