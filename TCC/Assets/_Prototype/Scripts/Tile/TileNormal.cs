using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNormal : Tile
{
    public TileNormal(Transform pivot)
    {
       
        Pivot = pivot;

    }

    public override void Interagir(PlayerController player)
    {
        player.speedTile = 0;
    }
}
