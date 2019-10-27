using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabuleiroControl : MonoBehaviour
{
    //public GameObject player; //test

    List<PlayerController> pl = new List<PlayerController>();
    PlayerController playerAtual;
    int pAtual;

    //int idAtual;
    public List<PosicoesTabuleiro> posTab = new List<PosicoesTabuleiro>();

    GameObject atual;
    GameObject destino;
    bool andando;
    //int direcao = 1;

    void Start()
    {
        andando = false;

        foreach (PlayerController p in FindObjectsOfType<PlayerController>())
        {
            pl.Add(p);
        }

        playerAtual = pl[0];

        //DefinirPosicoes();
    }

    void Update()
    {
        PodeAndar(atual, destino);
    }

    //void DefinirPosicoes()
    //{
    //    posTab.Clear();
    //    PosicoesTabuleiro[] pos;
    //    pos = GameObject.FindObjectsOfType<PosicoesTabuleiro>();

    //    foreach (PosicoesTabuleiro i in pos)
    //    {
    //        posTab.Add(i);
    //    }
    //}

    void AndarNoTabuleiro(int id, PlayerController p)
    {
        andando = true;

        if (p.GetComponent<DadoPlayer>().numDado + id >= posTab.Count)
            p.GetComponent<DadoPlayer>().direcaoPlayer = -1;
        if (p.GetComponent<DadoPlayer>().numDado - id <= 0)
            p.GetComponent<DadoPlayer>().direcaoPlayer = 1;

        p.GetComponent<DadoPlayer>().numDado += id * p.GetComponent<DadoPlayer>().direcaoPlayer;

        atual = p.gameObject;
        destino = posTab[p.GetComponent<DadoPlayer>().numDado].gameObject;

    }

    public void JogarDado()
    {
        if (!andando)
        {
            JogadorQueJoga();

            int posicaoDado = Random.Range(1, 7);
            if (playerAtual.GetComponent<DadoPlayer>().numDado + posicaoDado > posTab.Count)
                posicaoDado = 1;
            else if (playerAtual.GetComponent<DadoPlayer>().numDado - posicaoDado < 0 && playerAtual.GetComponent<DadoPlayer>().direcaoPlayer == -1)
                posicaoDado = 1;
            AndarNoTabuleiro(posicaoDado, playerAtual);
        }
    }

    void PodeAndar(GameObject a, GameObject b)
    {
        if (andando)
        {
            a.transform.position = Vector3.Lerp(a.transform.position, b.transform.position, Time.deltaTime * 5);
            if (Vector3.Distance(a.transform.position, b.transform.position) < 0.2f)
                andando = false;
        }
    }

    void JogadorQueJoga()
    {
        playerAtual = pl[pAtual];
        pAtual ++;

        if (pAtual >= pl.Count)
            pAtual = 0;
    }
}
