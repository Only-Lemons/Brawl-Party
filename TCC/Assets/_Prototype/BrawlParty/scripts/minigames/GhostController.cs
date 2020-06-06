using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    MiniGame minigame;
    NavMeshAgent thisAgent;
    void Start()
    {
        minigame = GameObject.Find("GameMode").GetComponent<GhostRun>();
        thisAgent = GetComponent<NavMeshAgent>();
    }
    public void FollowPlayer(PlayerController player)
    {
        if (player.gameObject != null)
        {
            this.thisAgent.ResetPath();
            this.thisAgent.SetDestination(player.transform.position);
        }
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
            minigame.HitRule(other.gameObject.GetComponent<PlayerController>());
    }

}
