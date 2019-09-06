using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma : ScriptableObject
{
    public float fireRate;
    public float ammoAmount;
    public float damage;

    public abstract void Shoot();
    

}
