using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBuraco : Tile, IInteractable
{

    public new void Interagir(Personagem personagem)
    {
        personagem.Morrer();
    }
    
}
