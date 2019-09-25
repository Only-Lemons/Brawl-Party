using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponentInParent<PlayerController>().withFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)  
    {
        if(other.tag == "Player")
        {
            other.GetComponentInParent<PlayerController>().withFlag = false;
        }
    }

}
