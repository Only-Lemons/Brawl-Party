using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bujing : Arma
{
    public Bujing()
    {
        fireRate = 0.50f;
        ammoAmount = 14;
        damage = 10;
        gunSprite = Resources.Load("Armas/Sprites/Pistol") as Sprite;
        prefab = Resources.Load("Armas/Pistol") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilBujing") as GameObject;
        ammunitionPrefab.GetComponent<BulletBujing>().damage = this.damage;

    }

  

    public override void Shoot(Vector3 position, Quaternion rotation, Vector3 Foward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletBujing>().transformForward = Foward;
        ammunitionPrefab.GetComponent<BulletBujing>().player = player;
        Instantiate(ammunitionPrefab,position + player.transform.forward*1.5f,rotation);
    }
}
