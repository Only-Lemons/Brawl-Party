using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TabuleiroControl : MonoBehaviour
{
    public static TabuleiroControl tabControl;
    List<PlayerController> pl = new List<PlayerController>();
    PlayerController playerAtual;
    int pAtual;

    public List<PosicoesTabuleiro> posTab = new List<PosicoesTabuleiro>();

    GameObject atual;
    GameObject destino;
    bool andando;

    int posicaoDado;

    public bool travarTudo;

    void Start()
    {
        tabControl = this;

        travarTudo = true;
        andando = false;

        DefinirJogadores();

        DefinirPosicoes();
    }

    void Update()
    {
        PodeAndar(atual, destino);
    }

    void DefinirJogadores()
    {
        foreach (PlayerController p in FindObjectsOfType<PlayerController>())
        {
            pl.Add(p);
        }

        playerAtual = pl[0];
    }

    void DefinirPosicoes()
    {
        posTab.Clear();
        
        PosicoesTabuleiro[] posOrd = GameObject.FindObjectsOfType<PosicoesTabuleiro>();
        posOrd = posOrd.OrderBy(x => x.id).ToArray();
        foreach(PosicoesTabuleiro p in posOrd)
        {
            posTab.Add(p);
        }
    }

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
        Dado.dadoControl.Jogar();
    }

    public void PodeIr()
    {
        if (!andando && !travarTudo)
        {
            JogadorQueJoga();

            posicaoDado = Dado.dadoControl.dadoValor;

            if (playerAtual.GetComponent<DadoPlayer>().numDado + posicaoDado > posTab.Count)
                posicaoDado = 1;
            else if (playerAtual.GetComponent<DadoPlayer>().numDado - posicaoDado < 0 && playerAtual.GetComponent<DadoPlayer>().direcaoPlayer == -1)
                posicaoDado = 1;

            AndarNoTabuleiro(posicaoDado, playerAtual);
            travarTudo = true;
        }
    }


    void PodeAndar(GameObject a, GameObject b)
    {
        if (andando && Dado.dadoControl.dadoParado)
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
