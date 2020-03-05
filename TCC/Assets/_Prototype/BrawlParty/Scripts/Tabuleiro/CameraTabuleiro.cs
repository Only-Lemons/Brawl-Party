using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTabuleiro : MonoBehaviour
{
    public static CameraTabuleiro camTab;
    Camera cam;

    Vector3 posicaoFocoFinal;
    Vector3 posicaoFoco;

    Vector3 posicaoCam;
    Vector3 posicaoCamFinal;

    void Start()
    {
        if (camTab == null)
            camTab = this;

        cam = Camera.main;

        PosicaoInicial();
    }

    void Update()
    {
        if (TabuleiroControl.tabControl.comecou)
            FocoEterno();
    }

    public void FocoNoJogador(GameObject jogador)
    {
        posicaoFocoFinal = jogador.transform.position;
        posicaoCamFinal = new Vector3(jogador.transform.position.x, jogador.transform.position.y + 10, jogador.transform.position.z + 5);
    }

    public void FocoNoDado(GameObject dado)
    {
        posicaoFocoFinal = dado.transform.position;
        posicaoCamFinal = new Vector3(dado.transform.position.x, dado.transform.position.y + 7, dado.transform.position.z + 2.5f);
    }

    public void FocoNoProximoJogador(GameObject j)
    {
        posicaoFocoFinal = j.transform.position;
        posicaoCamFinal = new Vector3(j.transform.position.x, j.transform.position.y + 3, j.transform.position.z + 2);
    }

    public void PosicaoInicial()
    {
        posicaoCam = transform.position;
        posicaoCamFinal = posicaoCam;
        posicaoFoco = Vector3.zero;
    }

    public void FocoEterno()
    {


        if (Vector3.Distance(posicaoFoco, posicaoFocoFinal) < 0.01f)
            posicaoFoco = posicaoFocoFinal;
        else
            posicaoFoco = Vector3.Lerp(posicaoFoco, posicaoFocoFinal, Time.deltaTime * 3);

        if (Vector3.Distance(posicaoCam, posicaoCamFinal) < 0.01f)
            posicaoCam = posicaoCamFinal;
        else
            posicaoCam = Vector3.Lerp(posicaoCam, posicaoCamFinal, Time.deltaTime * 3);

        cam.transform.position = posicaoCam;
        cam.transform.LookAt(posicaoFoco);
    }
}
