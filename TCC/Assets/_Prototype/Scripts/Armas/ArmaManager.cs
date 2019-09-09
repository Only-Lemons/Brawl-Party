using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaManager : MonoBehaviour
{
   public GameObject armaPrefab;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1)) //Provisorio
            instanciarArma();
    }


   public void instanciarArma()
   {
       int rnd = Random.Range(0,5);
       switch(rnd)
       {
            case 0:
                armaPrefab.GetComponent<ArmaController>().actualArma = new Pistol();
                break;
            case 1:
             armaPrefab.GetComponent<ArmaController>().actualArma = new Pistol();
                break;
  
            case 2:
             armaPrefab.GetComponent<ArmaController>().actualArma = new Shotgun();
                break;
  
            case 3:
             armaPrefab.GetComponent<ArmaController>().actualArma = new Shotgun();
                break;
  
            case 4:
             armaPrefab.GetComponent<ArmaController>().actualArma = new Pistol();
                break;

            default:
                break;
       }

       Instantiate(armaPrefab);
   }
}
