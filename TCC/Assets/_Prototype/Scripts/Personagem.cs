using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Correndo,
    Normal,
    Slow,
    Stun,
    Escorregadio
}
public class Personagem : MonoBehaviour
{
    public float velocidadedeMovimento;
    public State EstadoAtual;

    private void Start()
    {
        MudarEstado(State.Normal);
    }

    private void FixedUpdate()
    {
        Tile[] Tiles = TerrainController.SingletonTiles;
        IInteractable MenorPontoMedio = Tiles[0].GetComponent<IInteractable>();
        float menorDistancia = 100;
        for (int k = 0; k < Tiles.Length; k++)
        {
            if (Vector3.Distance(this.transform.position, Tiles[k].transform.position) < menorDistancia)
            {
                menorDistancia = Vector3.Distance(this.transform.position, Tiles[k].transform.position);
                MenorPontoMedio = Tiles[k].GetComponent<IInteractable>();
            }
        }
        MenorPontoMedio.Interagir(this);
    }

    public void MudarEstado(State state)
    {
        EstadoAtual = state;
        MudarAtributosPorEstado();
    }
    private void MudarAtributosPorEstado()
    {
        if (EstadoAtual == State.Normal)
        {
            velocidadedeMovimento = 1;
        }
        else if (EstadoAtual == State.Slow)
        {
            velocidadedeMovimento = 0.5f;
        }
        else if (EstadoAtual == State.Stun)
        {
            velocidadedeMovimento = 0;
        }
        else if (EstadoAtual == State.Correndo)
        {
            velocidadedeMovimento = 1.5f;
        }
        else if (EstadoAtual == State.Escorregadio)
        {
            velocidadedeMovimento = 2f;
        }
    }

    public void Morrer()
    {
        Destroy(this.gameObject);
    }
}
