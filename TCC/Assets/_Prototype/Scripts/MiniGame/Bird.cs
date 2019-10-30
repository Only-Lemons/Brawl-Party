using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            GameController.singleton.gameMode.HitRule(other.GetComponent<PlayerController>());
            Destroy(this.gameObject);
        }
    }
}
