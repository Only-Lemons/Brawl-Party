using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public Transform Pivot;

    public abstract void Interagir(PlayerController player);
}
