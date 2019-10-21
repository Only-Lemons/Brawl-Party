using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Congelante : Arma
{
    public Congelante()
    {
        fireRate = 2.0f;
        ammoAmount = 5;
        damage = 10;
        gunSprite = Resources.Load("Armas/Sprites/Pistol") as Sprite;
        prefab = Resources.Load("Armas/Pistol") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilCongelante") as GameObject;
        ammunitionPrefab.GetComponent<BulletCongelante>().damage = this.damage;

    }

  

    public override void Shoot(Vector3 position, Quaternion rotation, Vector3 Foward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletCongelante>().transformForward = Foward;
        ammunitionPrefab.GetComponent<BulletCongelante>().player = player;
        Instantiate(ammunitionPrefab,position + player.transform.forward*1.5f,rotation);
    }
}
