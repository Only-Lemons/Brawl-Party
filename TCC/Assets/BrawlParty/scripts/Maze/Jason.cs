using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Jason: MonoBehaviour
{
    NavMeshAgent IAmoviment;
    PlayerController target;
       
    void Start()
    {
        IAmoviment = GetComponent<NavMeshAgent>();
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            player.actualGameMode.HitRule(player);
        }
    }
    public void moviment(Vector3 vec)
    {
        IAmoviment.ResetPath();

        if(Vector3.Distance(this.gameObject.transform.position, vec) > 2)
        IAmoviment.SetDestination(vec);
    }
}
