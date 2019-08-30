using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNeve : Tile, IInteractable
{
    public new void Interagir(Personagem personagem)
    {
        personagem.MudarEstado(State.Slow);
    }
}
