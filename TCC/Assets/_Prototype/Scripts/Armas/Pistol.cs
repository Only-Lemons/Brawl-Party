using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Arma
{
    public Pistol()
    {
        fireRate = 1f;
        ammoAmount = 7;
        damage = 10;
        prefab = Resources.Load("Armas/Pistol") as GameObject;
    }

    public override void Shoot()
    {
        ammoAmount--;
        Debug.Log("PIUPIU");
   
    }
}
