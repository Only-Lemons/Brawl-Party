using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            MiniGame m = GameObject.Find("Game Mode").GetComponent<FallingGloves>();
            m.HitRule(player);
            //other.GetComponent<PlayerController>().ReceiveDamage(10000000, null);
        }
    }
   
}
