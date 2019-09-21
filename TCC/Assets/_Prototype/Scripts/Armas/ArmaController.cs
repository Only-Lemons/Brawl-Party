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
           if (other.GetComponentInParent<PlayerController>().actualArma == null)
           {
                other.GetComponentInParent<PlayerController>().actualArma = actualArma;
                other.GetComponentInParent<PlayerController>().playerUI.ammo.maxValue = actualArma.ammoAmount;
                Instantiate(actualArma.prefab, other.transform.GetChild(2).transform.position, other.transform.GetChild(2).rotation, other.transform.GetChild(2).transform);
             
            }
            else 
            {
                //other.GetComponentInParent<PlayerController>().arma
            }    
            Destroy(this.gameObject);
        }  

    }
}
