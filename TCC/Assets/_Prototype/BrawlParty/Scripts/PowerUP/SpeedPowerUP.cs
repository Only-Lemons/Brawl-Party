using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUP : PowerUP
{
    bool _activate = false;

    public override void FinishAndBack(PlayerController player)
    {
        player.speed -= player.player.speed;
    }
    public override void Interact(PlayerController player)
    {
        if (_activate == false)
        {
            player.speed += player.player.speed;
            _activate = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponentInParent<PlayerController>().AtivarPowerUP(2, null, this);
            Destroy(this.gameObject);
        }
    }

}
