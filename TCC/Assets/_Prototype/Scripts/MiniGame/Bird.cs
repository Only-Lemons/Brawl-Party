using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameController.singleton.gameMode.HitRule(other.GetComponent<PlayerController>()); 
            this.GetComponent<ParticlePlayer>().Play(.2F);
            
            Destroy(this.gameObject, .2F);
        }
    }
}
