using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{

    [SerializeField]
    JhonBean gameMode;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            gameMode.PointRule(other.GetComponent<PlayerController>());
        }
    }
}
