using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Passiva", menuName = "Passivas")]
public abstract class SOPassive : ScriptableObject
{
    float time;
    bool isActive;


    public abstract void AtivarPassiva(PlayerController player);

}
