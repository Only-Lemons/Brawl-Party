﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Jason: MonoBehaviour
{
    NavMeshAgent IAmoviment;
    PlayerController target;
    // Start is called before the first frame update
    void Start()
    {
        IAmoviment = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void pursuit()
    {

    }
    void gapPlayer(PlayerController player)
    {

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
