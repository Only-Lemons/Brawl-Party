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

        p.GetComponent<DadoPlayer>().numDado += id;

        atual = p.gameObject;
        proximaPos = posTab[(p.GetComponent<DadoPlayer>().posAtual + p.GetComponent<DadoPlayer>().direcaoPlayer)];
        destino = null;
        int valor = p.GetComponent<DadoPlayer>().numDado + p.GetComponent<DadoPlayer>().posAtual;
        Debug.Log(valor);

        if (valor >= posTab.Count || valor <= 0)
        {
            int resto;
            resto = valor - (posTab.Count % p.GetComponent<DadoPlayer>().posAtual);
            Debug.Log(resto);
            destino = posTab[resto];
        }
        else
            destino = posTab[valor];

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
                posicaoDado = playerAtual.GetComponent<DadoPlayer>().numDado - (playerAtual.GetComponent<DadoPlayer>().numDado % posTab.Count);
            else if (playerAtual.GetComponent<DadoPlayer>().numDado - posicaoDado < 0 && playerAtual.GetComponent<DadoPlayer>().direcaoPlayer == -1)
                posicaoDado = playerAtual.GetComponent<DadoPlayer>().numDado - (playerAtual.GetComponent<DadoPlayer>().numDado % posTab.Count);

            AndarNoTabuleiro(posicaoDado, playerAtual);

            travarTudo = true;
        }
    }

    void ContinueAndando(GameObject a, PosicoesTabuleiro f)
    {
        if (andando && Dado.dadoControl.dadoParado)
        {
            if (a.GetComponent<DadoPlayer>().posAtual >= posTab.Count - 1)
                a.GetComponent<DadoPlayer>().direcaoPlayer = -1;
            else if (a.GetComponent<DadoPlayer>().posAtual <= 0)
                a.GetComponent<DadoPlayer>().direcaoPlayer = 1;

            a.GetComponent<DadoPlayer>().posAtual = proximaPos.id;

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

                    proximaPos = posTab[proximaPos.GetComponent<PosicoesTabuleiro>().id + 1 * a.GetComponent<DadoPlayer>().direcaoPlayer];
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
