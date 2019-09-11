using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Passive : ScriptableObject
{
    float time;
    bool isActive;


    public abstract void AtivarPassiva(PlayerController player);

}
