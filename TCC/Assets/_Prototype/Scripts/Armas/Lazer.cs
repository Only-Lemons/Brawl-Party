using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lazer : Arma
{
    public Lazer()
    {
        fireRate = 0.05f;
        ammoAmount = 140;
        damage = 1;
        gunSprite = Resources.Load<Sprite>("Armas/Sprites/Shotgun");
        prefab = Resources.Load("Armas/Armas/Prefabs/Lazer") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilLazer") as GameObject;
        ammunitionPrefab.GetComponent<BulletLazer>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletLazer>().transformForward = Forward;
        ammunitionPrefab.GetComponent<BulletLazer>().player = player;
        GameObject ob = Instantiate(ammunitionPrefab, position + player.transform.forward * 2f, rotation);
    }
}
