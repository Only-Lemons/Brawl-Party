using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Tile 
{
    public int Peso;
    public Transform Pivot;
    public GameObject Prefab;

    public abstract void Interagir(Personagem player);


}
