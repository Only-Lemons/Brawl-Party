using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGelo : Tile
{
    public TileGelo(Transform pivot)
    {
        Peso = 1;
        Pivot = pivot;
        Prefab = Resources.Load("Tiles/TileGelo") as GameObject;
    }

    public override void Interagir(Personagem player)
    {
        player.MudarEstado(State.Escorregadio);
    }

}
