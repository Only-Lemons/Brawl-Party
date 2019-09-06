using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Armas/Pistol")]
public class Pistol : Arma
{
    void Awake()
    {
        fireRate = 2;
        ammoAmount = 5;
        damage = 200;
    }
    public override void Shoot()
    {
        Debug.Log("PIUPIU");
    }
}
