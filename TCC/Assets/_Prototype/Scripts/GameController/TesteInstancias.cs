using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteInstancias : MonoBehaviour
{
    public static TesteInstancias testeI;
    List<GameObject> tilesN = new List<GameObject>();

    public GameObject[] go;
    public int velocidadeQueda = 10;

    Vector2 limiteParaInstanciar = new Vector2(15, 15); //Modo Random ou sem tiles

    GameObject cxPadrao;

    //Vector3 pontoRef;
    float timer;
    public int tempoRespawn = 2;

    public int qtdInstanciar = 1;

    void Start()
    {
        testeI = this;

        cxPadrao = Resources.Load("Bau/Bau") as GameObject;

        GetNormalTiles();

        timer = tempoRespawn;
        //pontoRef = Vector3.zero;
    }

    void Update()
    {
        Instanciar();
    }

    void InstanciarCaixaPorTile()
    {
        for (int i = 0; i < qtdInstanciar; i++)
        {
            int posO = Random.Range(0, tilesN.Count);
            GameObject go1 = tilesN[posO];
            Vector3 novoPos = go1.transform.position;
            novoPos.y = 20;

            Instantiate(cxPadrao, novoPos, Quaternion.identity);
        }
    }

    void InstanciarCaixaSemTile()
    {
        for (int i = 0; i < qtdInstanciar; i++)
        {
            int x = (int)Random.Range(-limiteParaInstanciar.x, limiteParaInstanciar.x + 1);
            int z = (int)Random.Range(-limiteParaInstanciar.y, limiteParaInstanciar.y + 1);
            Vector3 pontoRef = new Vector3(x, 20, z);
            Instantiate(cxPadrao, pontoRef, Quaternion.identity);
        }
    }

    void Instanciar()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = tempoRespawn;
            InstanciarCaixaPorTile();
        }
    }

    public GameObject InstanciarArma(int b)
    {
        int contador = 3; //Usado para aumentar chances de drop da pistola comum em reação às outras armas
        int rnd = Random.Range(0, 10);
        if (rnd != 0)
            for (int i = 0; i <= contador; i++)
            {
                rnd = Random.Range(0, 10);
                if (rnd == 0)
                    break;
            }
        //rnd = 9;

        switch (rnd)
        {
            case 0:
                go[b].GetComponent<ArmaController>().actualArma = new Pistol();
                break;
            case 1:
                go[b].GetComponent<ArmaController>().actualArma = new Lazer();
                break;
            case 2:
                go[b].GetComponent<ArmaController>().actualArma = new Shotgun();
                break;
            case 3:
                go[b].GetComponent<ArmaController>().actualArma = new Metralhadora();
                break;
            case 4:
                go[b].GetComponent<ArmaController>().actualArma = new LancaGranada();
                break;
            case 5:
                go[b].GetComponent<ArmaController>().actualArma = new Bazooka();
                break;
            case 6:
                go[b].GetComponent<ArmaController>().actualArma = new Bujing();
                break;
            case 7:
                go[b].GetComponent<ArmaController>().actualArma = new Booming();
                break;
            case 8:
                go[b].GetComponent<ArmaController>().actualArma = new Congelante();
                break;
            case 9:
                go[b].GetComponent<ArmaController>().actualArma = new Doomoo();
                break;
            case 10:
                go[b].GetComponent<ArmaController>().actualArma = new Teletiroporte(); //Erros
                break;

            default:
                break;
        }
        return go[b];
    }



    void GetNormalTiles()
    {
        tilesN.Clear();
        GameObject[] aux = GameObject.FindGameObjectsWithTag("TileN");
        foreach (GameObject item in aux)
        {
            tilesN.Add(item);
        }
    }

}
