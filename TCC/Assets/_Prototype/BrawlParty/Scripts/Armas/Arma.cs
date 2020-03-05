using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma : ScriptableObject
{
    public float fireRate;
    public float ammoAmount;
    public int damage;
    public GameObject ammunitionPrefab;
    public Sprite gunSprite;
    public GameObject prefab;
    public AudioClip audio;

    public Arma()
    {
        fireRate = 0;
        ammoAmount = 0;
        damage = 0;
        ammunitionPrefab = null;
        gunSprite = null;
        prefab = null;
        audio = null;
    }

    public abstract void Shoot(Vector3 position,Quaternion rotation,Vector3 Foward, PlayerController player);
    

}
