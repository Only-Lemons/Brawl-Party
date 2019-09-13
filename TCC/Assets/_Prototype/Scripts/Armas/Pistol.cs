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
    }

    public override void Shoot(Transform pos)
    {
        ammoAmount--;
        
        Instantiate(ammunitionPrefab,pos.position,Quaternion.identity);
   
    }
}
