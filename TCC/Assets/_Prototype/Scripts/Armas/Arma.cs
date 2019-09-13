using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma : ScriptableObject
{
    public float fireRate;
    public float ammoAmount;
    public int damage;
    public GameObject ammunitionPrefab;

    public GameObject prefab;

    public abstract void Shoot();
    

}
