using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuraco : Tile
{
    public TileBuraco(Transform pivot)
    {
        Peso = 2;
        Pivot = pivot;
        Prefab = Resources.Load("Tiles/TileBuraco") as GameObject;
    }

    public override void Interagir(Personagem player)
    {
        player.MudarEstado(State.Stun);
    }
}
