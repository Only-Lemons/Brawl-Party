using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadFB : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if(!TimeGameController.Instance.Acabou())
                player.actualGameMode.HitRule(player);
        }
    }

}
