using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PlayerController>() != null)
            GameController.singleton.gameMode.HitRule(other.gameObject.GetComponentInParent<PlayerController>());
    }
}
