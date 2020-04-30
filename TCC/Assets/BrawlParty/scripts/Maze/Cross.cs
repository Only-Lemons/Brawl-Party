using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cross : MonoBehaviour
{
    public GhostRun modegame;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.gameObject.GetComponentInChildren<PlayerController>();
        if (player != null)
        {
            modegame.AddObjectInPlayer(player);
            GameObject.Destroy(this.gameObject);
        }
    }
}
