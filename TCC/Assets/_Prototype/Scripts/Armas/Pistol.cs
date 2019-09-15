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
        ammunitionPrefab = Resources.Load("Municoes/Projetil") as GameObject;
        ammunitionPrefab.GetComponent<Tiro>().damage = this.damage;
    }

  

    public override void Shoot(Vector3 position, Quaternion rotation)
    {
        ammoAmount--;
    
        Instantiate(ammunitionPrefab,position,rotation);
    }
}
