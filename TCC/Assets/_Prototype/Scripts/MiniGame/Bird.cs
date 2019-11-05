using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<ParticlePlayer>().Play(.5F);
            GameController.singleton.gameMode.HitRule(other.GetComponent<PlayerController>());
            Destroy(this.gameObject, .5F);
        }
    }
}
