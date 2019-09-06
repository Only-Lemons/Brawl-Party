using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerUP : PowerUP
{

    public override void Interact(PlayerController player)
    {
        Player aux = player.player;
        if (player.life < aux.hp)
            player.life += 1;

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerController>() != null)
        {
            other.GetComponentInParent<PlayerController>().AtivarPowerUP(10, null, this);
            Destroy(this.gameObject);
        }
        Debug.Log(other.gameObject);
    }

}
