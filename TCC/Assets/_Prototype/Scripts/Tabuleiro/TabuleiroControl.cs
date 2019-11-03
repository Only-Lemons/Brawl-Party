using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TabuleiroControl : MonoBehaviour
{
    public static TabuleiroControl tabControl;
    List<PlayerController> pl = new List<PlayerController>();
    PlayerController playerAtual;
    int playerAtualId;

    public List<PosicoesTabuleiro> posTab = new List<PosicoesTabuleiro>();

    GameObject atual;
    public PosicoesTabuleiro proximaPos;
    PosicoesTabuleiro destino;
    bool andando;

    int posicaoDado;

    public bool travarTudo;
    [HideInInspector]
    public bool comecou;

    void Start()
    {
        tabControl = this;

        travarTudo = true;
        andando = false;
        comecou = false;
        recebeBonus = false;

        DefinirJogadores();

        DefinirPosicoes();
    }

    void Update()
    {
        //PodeAndar(atual, destino);
        ContinueAndando(atual, destino);
        PausaParaReceberBonus();

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
        foreach (PosicoesTabuleiro p in posOrd)
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
        proximaPos = posTab[(p.GetComponent<DadoPlayer>().posAtual + (p.GetComponent<DadoPlayer>().numDado - (p.GetComponent<DadoPlayer>().numDado - 1)))];
        destino = posTab[p.GetComponent<DadoPlayer>().numDado];

    }

    public void JogarDado()
    {
        if (andando == false)
        {
            comecou = true;
            Dado.dadoControl.Jogar();
        }
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

    void ContinueAndando(GameObject a, PosicoesTabuleiro f)
    {
        if (andando && Dado.dadoControl.dadoParado)
        {
            a.transform.position = Vector3.Lerp(a.transform.position, proximaPos.transform.position, Time.deltaTime * 5);
            CameraTabuleiro.camTab.FocoNoJogador(atual);

            if (Vector3.Distance(a.transform.position, proximaPos.transform.position) < 0.2f)
            {
                if (proximaPos.transform.position == f.transform.position)
                {
                    a.GetComponent<DadoPlayer>().posAtual = f.id;
                    proximaPos.ReceberBonus(playerAtual);
                    recebeBonus = true;
                    andando = false;
                    return;
                }
                else
                {
                    proximaPos = posTab[proximaPos.GetComponent<PosicoesTabuleiro>().id + 1];
                    return;
                }
            }
        }

        else if (!andando && Dado.dadoControl.dadoParado && comecou && !recebeBonus)
        {
            CameraTabuleiro.camTab.FocoNoProximoJogador(pl[playerAtualId].gameObject);
        }
    }

    void JogadorQueJoga()
    {
        playerAtual = pl[playerAtualId];
        playerAtualId++;

        if (playerAtualId >= pl.Count)
            playerAtualId = 0;
    }

    float pInicial = 0;
    bool recebeBonus;
    void PausaParaReceberBonus()
    {
        if (recebeBonus)
        {
            pInicial += Time.deltaTime;
            if (pInicial >= 2)
            {
                recebeBonus = false;
                pInicial = 0;
            }
        }
    }
}
