﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Armas/Shotgun")]
public class Shotgun : Arma
{

    public override void Shoot()
    {
        Debug.Log("PLAUPLAU");
    }
}