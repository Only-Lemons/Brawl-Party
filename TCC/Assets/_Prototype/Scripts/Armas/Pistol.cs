using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="Armas/Pistol")]
public class Pistol : Arma
{
    public override void Shoot()
    {
        Debug.Log("PIUPIU");
    }
}
