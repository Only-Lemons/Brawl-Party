using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colmeia : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "point")
        {
            GameController.singleton.gameMode.PointRule(other.GetComponent<Basket>().player);
            Destroy(other.gameObject);
        }
    }
}
