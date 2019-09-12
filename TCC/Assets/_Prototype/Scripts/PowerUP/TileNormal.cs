using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNormal : Tile
{
    public TileNormal(Transform pivot)
    {
        Peso = 1;
        Pivot = pivot;
        Prefab = Resources.Load("Tiles/TileNormal") as GameObject;
    }

    public override void Interagir(Personagem player)
    {
        player.MudarEstado(State.Normal);
    }
}
