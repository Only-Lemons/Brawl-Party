using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileQuebravel : Tile
{
    public TileQuebravel(Transform pivot)
    {
   
        Pivot = pivot;
     
    }

    public  override void Interagir(Personagem personagem)
    {
        personagem.MudarEstado(State.Slow);
    }
}
