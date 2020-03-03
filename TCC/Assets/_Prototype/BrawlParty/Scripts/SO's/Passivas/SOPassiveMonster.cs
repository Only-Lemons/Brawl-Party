using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Passivas", menuName = "Passivas/PassivaMonster")]
public class SOPassiveMonster : SOPassive
{
    public int recoveryTime;
    float _timeTotal = 0;
    float _time = 1;

    public override void AtivarPassiva(PlayerController player)
    {
        SOPlayer aux = player.player;
        if (player.life <= (aux.hp * 0.30f) && CheckCD())
        {
            int vidaMaximaT = Mathf.FloorToInt(aux.hp * 0.3f/ recoveryTime);
            if (Cronometro())
            {
                player.life += vidaMaximaT;
                if (player.life > aux.hp)
                    player.life = aux.hp;
                if (_timeTotal == recoveryTime)
                {
                    inCD = true;
                    _timeTotal = 0;
                }
            }
        }
    }
    bool Cronometro()
    {
        _time -= Time.deltaTime;
        if(_time <= 0)
        {
            _time = 1;
            _timeTotal += 1;
            return true;
        }
        return false;
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
