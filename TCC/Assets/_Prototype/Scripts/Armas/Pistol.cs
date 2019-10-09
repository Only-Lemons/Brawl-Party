using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Arma
{
    public Pistol()
    {
        fireRate = .2f;
        ammoAmount = 27;
        damage = 10;
        gunSprite = Resources.Load("Armas/Sprites/Pistol") as Sprite;
        prefab = Resources.Load("Armas/Pistol") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/Projetil") as GameObject;
        ammunitionPrefab.GetComponent<Bullet>().damage = this.damage;
    }

  

    public override void Shoot(Vector3 position, Quaternion rotation, Vector3 Foward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<Bullet>().transformForward = Foward;
        ammunitionPrefab.GetComponent<Bullet>().player = player;
        Instantiate(ammunitionPrefab,position + player.transform.forward*1.5f,rotation);
    }
}
