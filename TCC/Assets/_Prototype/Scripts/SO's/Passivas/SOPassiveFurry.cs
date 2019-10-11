using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passivas", menuName = "Passivas/PassivaFurry")]
public class SOPassiveFurry : SOPassive
{
    Arma _armaAtual;
    bool _isActive = false;
    float _time = 2f;

    public override void AtivarPassiva(PlayerController player)
    {
        if (Cronometro())
            FinishPassive(player);
        if (player.actualArma != null && _isActive == false && CheckCD())
        {
            if (_armaAtual != player.actualArma)
            {
                _armaAtual = player.actualArma;
                player.speed += player.player.speed/2;
                _isActive = true;
            }
        }
        if (player.PowerUp && _isActive == false && CheckCD())
        {
            _armaAtual = player.actualArma;
            player.speed += player.player.speed / 2;
            _isActive = true;
        }
    }
    void FinishPassive(PlayerController player)
    {
        _isActive = false;
        player.speed -= player.player.speed/2;
        inCD = true;
    }
    bool Cronometro()
    {
        if (_isActive == true)
        {
            _time -= Time.deltaTime;
            if (_time <= 0)
            {
                _time = 2f;
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
