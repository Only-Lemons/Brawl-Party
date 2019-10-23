using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaController : MonoBehaviour
{
   public Arma actualArma;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController playerController = other.GetComponentInParent<PlayerController>();

           //if (playerController.actualArma == null)
           {
                playerController.actualArma = null;
                playerController.actualArma = actualArma;
                playerController.playerUI.ammo.maxValue = actualArma.ammoAmount;
                playerController.anim.SetBool("HasGun",true);
                Instantiate(actualArma.prefab, playerController.hand.position, Quaternion.identity, playerController.hand.transform);
             
            }
           // else 
            {
                //other.GetComponentInParent<PlayerController>().arma
            }    
            Destroy(this.gameObject);
        }  

    }
}
