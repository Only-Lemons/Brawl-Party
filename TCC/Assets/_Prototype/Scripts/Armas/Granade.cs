using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Granade : Arma
{
    public Granade()
    {
        fireRate = 1f;
        ammoAmount = 1;
        damage = 100;
        gunSprite = Resources.Load("Armas/Sprites/Shotgun") as Sprite;
        prefab = Resources.Load("Armas/Shotgun") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilBazuca") as GameObject;
        ammunitionPrefab.GetComponent<BulletBazuca>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletBazuca>().transformForward = Forward;
        ammunitionPrefab.GetComponent<BulletBazuca>().player = player;
        GameObject ob = Instantiate(ammunitionPrefab, position, rotation);
    }
}
