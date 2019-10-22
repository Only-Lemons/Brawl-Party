using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teletiroporte : Arma
{
    public Teletiroporte()
    {
        fireRate = 2.0f;
        ammoAmount = 2;
        damage = 10;
        gunSprite = Resources.Load("Armas/Sprites/Pistol") as Sprite;
        prefab = Resources.Load("Armas/Pistol") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilTeletiroporte") as GameObject;
        ammunitionPrefab.GetComponent<BulletTeletiroporte>().damage = this.damage;

    }

  

    public override void Shoot(Vector3 position, Quaternion rotation, Vector3 Foward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletTeletiroporte>().transformForward = Foward;
        ammunitionPrefab.GetComponent<BulletTeletiroporte>().player = player;
        Instantiate(ammunitionPrefab,position + player.transform.forward*1.5f,rotation);
    }
}
