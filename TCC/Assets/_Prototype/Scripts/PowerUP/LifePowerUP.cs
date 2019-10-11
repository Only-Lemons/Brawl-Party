using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerUP : PowerUP
{
    float _time = 1;
    public override void FinishAndBack(PlayerController player) { }
    public override void Interact(PlayerController player)
    {
        SOPlayer aux = player.player;
        int vidaMaximaT = Mathf.FloorToInt(player.life +aux.hp * 0.20f)/3;
        _time += Time.deltaTime;
        if (player.life < aux.hp && _time >= 1)
        {
            player.life += vidaMaximaT;
            if (player.life > aux.hp)
                player.life = aux.hp;
            _time = 0;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponentInParent<PlayerController>().AtivarPowerUP(3, null, this);
            Destroy(this.gameObject);
        }
    }

}
