using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arma : ScriptableObject
{
    float fireRate;
    float ammoAmount;
    float damage;

    public abstract void Shoot();
    

}
