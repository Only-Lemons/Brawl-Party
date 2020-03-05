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
        gunSprite = Resources.Load<Sprite>("Armas/Sprites/Shotgun");
        prefab = Resources.Load("Armas/Shotgun") as GameObject;
        audio = Resources.Load("Municoes/Sons/Pistol") as AudioClip;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilGranada") as GameObject;
        ammunitionPrefab.GetComponent<BulletGranade>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletGranade>().transformForward = Forward;
        ammunitionPrefab.GetComponent<BulletGranade>().player = player;
        GameObject ob = Instantiate(ammunitionPrefab, position, rotation);
        player.armaSom.clip = audio;
        player.armaSom.Play();
    }
}
