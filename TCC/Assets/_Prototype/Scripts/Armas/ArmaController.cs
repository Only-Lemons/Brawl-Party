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
            other.GetComponentInParent<PlayerController>().actualArma = actualArma;
            Instantiate(actualArma.prefab,other.transform.GetChild(1).transform.position,Quaternion.identity,other.transform.GetChild(1).transform);
            Destroy(this.gameObject);
        }

    }
}
