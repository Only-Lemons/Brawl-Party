using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booming : Arma
{
    public Booming()
    {
        fireRate = 0.60f;
        ammoAmount = 7;
        damage = 20;
        gunSprite = Resources.Load<Sprite>("Armas/Sprites/Pistol");
        prefab = Resources.Load("Armas/Pistol") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilBooming") as GameObject;
        ammunitionPrefab.GetComponent<BulletBooming>().damage = this.damage;

    }

  

    public override void Shoot(Vector3 position, Quaternion rotation, Vector3 Foward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletBooming>().transformForward = Foward;
        ammunitionPrefab.GetComponent<BulletBooming>().player = player;
        Instantiate(ammunitionPrefab,position + player.transform.forward*1.5f,rotation);
    }
}
