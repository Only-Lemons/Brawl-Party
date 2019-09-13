using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOPassive : ScriptableObject
{
    public float timeCD;
    public float actualtimeCD;
    public bool inCD;


    public abstract void AtivarPassiva(PlayerController player);
    public abstract bool CheckCD();
}
