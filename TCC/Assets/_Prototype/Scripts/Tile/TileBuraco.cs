using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuraco : Tile
{
    public TileBuraco(Transform pivot)
    {
        Pivot = pivot;
    }

    public override void Interagir(PlayerController player)
    {
        player.speedTile = -player.speed;
    }
}
