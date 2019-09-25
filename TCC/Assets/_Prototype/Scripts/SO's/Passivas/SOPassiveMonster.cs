using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Passivas", menuName = "PassivaMonster")]
public class SOPassiveMonster : SOPassive
{
    float recoveryTime;
    float timeTotal = 0;
    float time = 1;
    public override void AtivarPassiva(PlayerController player)
    {
        recoveryTime = 3;
        SOPlayer aux = player.player;
        if (player.life <= (aux.hp * 0.30f) && CheckCD())
        {
            
            if (player.life > 0) {
                Debug.Log("Ola");
                int vidaMaximaT = Mathf.FloorToInt(aux.hp * 0.3f / recoveryTime);

                if (Cronometro())
                {
                    player.life += vidaMaximaT;
                    if (player.life > aux.hp)
                        player.life = aux.hp;
                    if (timeTotal >= recoveryTime)
                    {
                        inCD = true;
                        timeTotal = 0;
                    }
                }
            }
        }
    }
    bool Cronometro()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = 1;
            timeTotal += 1;
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
