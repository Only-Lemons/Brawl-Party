using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class  PowerUP : MonoBehaviour
{
    public string Name;
    public abstract void Interact(PlayerController player);
}
