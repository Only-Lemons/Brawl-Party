using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public int type;
    public PlayerController player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "point")
        {
            GameController.singleton.gameMode.PointRule(player);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "enemy")
        {
            GameController.singleton.gameMode.DeathRule(player);
            Destroy(other.gameObject);
        }
    }
}
