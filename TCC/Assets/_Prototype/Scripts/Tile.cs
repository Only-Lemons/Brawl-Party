using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IInteractable
{
    public int Peso;
    public GameObject Prefab;

    public void Interagir(Personagem personagem)
    {
        personagem.MudarEstado(State.Normal);
    }
}
