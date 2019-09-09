using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : Arma
{
    public Shotgun()
    {
        fireRate = 2f;
        ammoAmount = 4;
        damage = 20;
        prefab = Resources.Load("Armas/Shotgun") as GameObject;
       
    }

    public override void Shoot()
    {
        Debug.Log("PLAUPLAU");
    }
}
