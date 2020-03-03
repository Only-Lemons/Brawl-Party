using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bazooka : Arma
{
    public Bazooka()
    {
        fireRate = 1f;
        ammoAmount = 1;
        damage = 140;
        gunSprite = Resources.Load<Sprite>("Armas/Sprites/Shotgun");
        prefab = Resources.Load("Armas/Armas/Prefabs/Bazuca") as GameObject;
        audio = Resources.Load("Municoes/Sons/Pistol") as AudioClip;
        ammunitionPrefab = Resources.Load("Municoes/ProjetilBazuca") as GameObject;
        ammunitionPrefab.GetComponent<BulletBazuca>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<BulletBazuca>().transformForward = Forward;
        ammunitionPrefab.GetComponent<BulletBazuca>().player = player;
        GameObject ob = Instantiate(ammunitionPrefab, position + player.transform.forward * 2f, rotation);
        ob.transform.localScale = new Vector3(1.4f,1.4f,1.4f);
        player.armaSom.clip = audio;
        player.armaSom.Play();
    }
}
