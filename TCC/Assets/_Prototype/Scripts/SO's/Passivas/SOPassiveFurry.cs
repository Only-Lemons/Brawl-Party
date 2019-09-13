using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOPassiveFurry : SOPassive
{
    Arma armaAtual;
    bool isActive = false;
    float time = 2f;
    public override void AtivarPassiva(PlayerController player)
    {
        if (Cronometro())
            FinishPassive(player);
        if (player.actualArma != null && isActive == false)
        {
            if (armaAtual != player.actualArma)
            {
                armaAtual = player.actualArma;
                player.speed += player.player.speed;
                isActive = true;
            }

        }
        if(player.PowerUp && isActive == false)
        {
            armaAtual = player.actualArma;
            player.speed += player.player.speed;
            isActive = true;
        }

    }
    void FinishPassive(PlayerController player)
    {
        isActive = false;
        player.speed -= player.player.speed;
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

}
