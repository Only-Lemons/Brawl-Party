using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LancaGranada : Arma
{
    public LancaGranada()
    {
        fireRate = 2.5f;
        ammoAmount = 4;
        damage = 35;
        gunSprite = Resources.Load("Armas/Sprites/Shotgun") as Sprite;
        prefab = Resources.Load("Armas/Shotgun") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilGranada") as GameObject;
        ammunitionPrefab.GetComponent<BulletGranade>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletGranade>().transformForward = Forward;
        ammunitionPrefab.GetComponent<BulletGranade>().player = player;
        GameObject ob = Instantiate(ammunitionPrefab, position, rotation);
    }
}
