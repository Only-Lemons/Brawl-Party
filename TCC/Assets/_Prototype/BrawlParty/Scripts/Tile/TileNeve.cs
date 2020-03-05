using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNeve : Tile
{
    public TileNeve(Transform pivot)
    {
   
        Pivot = pivot;
     
    }

    public  override void Interagir(PlayerController personagem)
    {
        personagem.speedTile = -(personagem.player.speed/2);
    }
}
