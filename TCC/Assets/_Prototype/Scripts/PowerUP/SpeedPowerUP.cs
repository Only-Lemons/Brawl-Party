using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUP : PowerUP
{
    bool Activate = false;

    public override void FinishAndBack(PlayerController player)
    {
        player.speed = player.player.speed;
    }

    public override void Interact(PlayerController player)
    {

        if (Activate == false)
        {
            player.speed = player.speed + (player.speed * 0.30f);
            Activate = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<PlayerController>() != null)
        {
            other.GetComponentInParent<PlayerController>().AtivarPowerUP(5, null, this);
            Destroy(this.gameObject);
        }
        Debug.Log(other.gameObject);
    }

}
