﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileNeve : Tile
{
    public TileNeve(Transform pivot)
    {
        Peso = 1;
        Pivot = pivot;
        Prefab = Resources.Load("Tiles/TileNeve") as GameObject;
    }

    public  override void Interagir(Personagem personagem)
    {
        personagem.MudarEstado(State.Slow);
    }
}