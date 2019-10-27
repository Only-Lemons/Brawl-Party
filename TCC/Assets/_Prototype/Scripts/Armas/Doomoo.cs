using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Doomoo : Arma
{
    public Doomoo()
    {
        fireRate = 1f;
        ammoAmount = 1;
        damage = 1;
        gunSprite = Resources.Load<Sprite>("Armas/Sprites/Shotgun");
        prefab = Resources.Load("Armas/Armas/Prefabs/Doomoo") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilDoomoo") as GameObject;
        ammunitionPrefab.GetComponent<BulletDoomoo>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletDoomoo>().transformForward = Forward;
        ammunitionPrefab.GetComponent<BulletDoomoo>().player = player;
        GameObject ob = Instantiate(ammunitionPrefab, position + player.transform.forward * 2f, rotation);
        ob.transform.localScale = new Vector3(1.4f,1.4f,1.4f);
    }
}
