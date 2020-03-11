using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishMazeGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.actualGameMode.HitRule(player);
            Debug.Log(player.name +  " terminou o game");
        }
    }
}
