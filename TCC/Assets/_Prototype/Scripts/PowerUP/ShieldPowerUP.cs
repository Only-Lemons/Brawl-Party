using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUP : PowerUP
{
    bool Activate = false;

    public override void FinishAndBack(PlayerController player)
    {
        player.DesativarEscudo((int)(player.player.hp * 0.30f));
    }

    public override void Interact(PlayerController player)
    {
        if (Activate == false)
        {
            player.AtivarEscudo((int)(player.player.hp * 0.30f));
            Activate = true;
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
