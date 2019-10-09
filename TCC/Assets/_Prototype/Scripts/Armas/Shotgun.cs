using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shotgun : Arma
{
    public Shotgun()
    {
        fireRate = 1f;
        ammoAmount = 4;
        damage = 20;
        gunSprite = Resources.Load("Armas/Sprites/Shotgun") as Sprite;
        prefab = Resources.Load("Armas/Shotgun") as GameObject;
        ammunitionPrefab = Resources.Load("Municoes/Projetil") as GameObject;
        ammunitionPrefab.GetComponent<Bullet>().damage = this.damage;
       

    }

    public override void Shoot(Vector3 position, Quaternion rotation,Vector3 Forward, PlayerController player)
    {
        ammoAmount--;
        ammunitionPrefab.GetComponent<Bullet>().transformForward = Forward;
        ammunitionPrefab.GetComponent<Bullet>().player = player;
        Instantiate(ammunitionPrefab, position + player.transform.forward * 1.5f, rotation);
        Instantiate(ammunitionPrefab, position + player.transform.forward * 1.5f, new Quaternion(rotation.x,rotation.y - Random.Range(1f, 3f), rotation.z - Random.Range(1f,3f),rotation.w));
        Instantiate(ammunitionPrefab, position + player.transform.forward * 1.5f, new Quaternion(rotation.x, rotation.y + Random.Range(1f, 3f), rotation.z + Random.Range(1f,3f), rotation.w));

    }
}
