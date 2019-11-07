using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostController : MonoBehaviour
{
    NavMeshAgent thisAgent;
    void Start()
    {
        thisAgent = GetComponent<NavMeshAgent>();
    }
    public void FollowPlayer(PlayerController player)
    {
        this.thisAgent.ResetPath();
        this.thisAgent.SetDestination(player.transform.position);
      
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
            GameController.singleton.gameMode.HitRule(other.gameObject.GetComponent<PlayerController>());
    }

}
