using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            GameController.Singleton.gameMode.PointRule(other.GetComponent<PlayerController>());
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)  
    {
        if(other.tag == "Player")
        {
            GameController.Singleton.gameMode.PointRule(other.GetComponent<PlayerController>());
            Destroy(this.gameObject);
        }
    }

}
