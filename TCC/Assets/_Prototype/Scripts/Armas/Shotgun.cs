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
        ammunitionPrefab = Resources.Load("Municoes/Projetil") as GameObject;
        ammunitionPrefab.GetComponent<Bullet>().damage = this.damage;

    }

    public override void Shoot(Vector3 position, Quaternion rotation)
    {
        ammoAmount--;
        Instantiate(ammunitionPrefab, position, rotation);
        Instantiate(ammunitionPrefab, position, new Quaternion(rotation.x,rotation.y,rotation.z -2,rotation.w));
        Instantiate(ammunitionPrefab, position, new Quaternion(rotation.x, rotation.y, rotation.z + 2, rotation.w));

    }
}
