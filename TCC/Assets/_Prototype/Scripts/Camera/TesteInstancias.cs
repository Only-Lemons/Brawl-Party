using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteInstancias : MonoBehaviour
{
    public List<GameObject> tilesN = new List<GameObject>();

    public GameObject[] go;
    public int velocidadeQueda = 10;
    public Vector2 limiteParaInstanciar = new Vector2(15, 15);
    public GameObject cxPadrao;

    GameObject goAtual;
    Vector3 pontoRef;
    float timer;
    public int tempoRespawn = 2;

    public int qtdInstanciar = 1;

    void Start()
    {
        GetNormalTiles();

        goAtual = null;
        timer = tempoRespawn;
        pontoRef = Vector3.zero;
    }

    void Update()
    {
        Instanciar();
        ControlarQueda();
    }

    void InstanciarCaixa()
    {
        //Modelo inicial
        int x = (int)Random.Range(-limiteParaInstanciar.x, limiteParaInstanciar.x + 1);
        int z = (int)Random.Range(-limiteParaInstanciar.y, limiteParaInstanciar.y + 1);
        pontoRef = new Vector3(x, 20, z);
        //Fim

        //Modelo por tile normal
        for(int i =0; i< qtdInstanciar; i++)
        {
        int posO = 0;
        Vector3 novoPos = Vector3.zero;
        GameObject go1 = null;

        posO = Random.Range(0, tilesN.Count);
        go1 = tilesN[posO];
        novoPos = go1.transform.position;
        novoPos.y = 20;
        //Fim

        if (goAtual == null)
        {
            //goAtual = Instantiate(cxPadrao, pontoRef, Quaternion.identity); //Modelo inicial
            
            goAtual = Instantiate(cxPadrao, novoPos, Quaternion.identity);
             //Modelo por tile normal

        }
        }
    }

    void Instanciar()
    {
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = tempoRespawn;
            InstanciarCaixa();
            InstanciarCaixa();
            InstanciarCaixa();
        }
    }

    public GameObject InstanciarArma(int b)
    {
        int rnd = Random.Range(0, 5);
        switch (rnd)
        {
            case 0:
                go[b].GetComponent<ArmaController>().actualArma = new Bazooka();
                break;
            case 1:
                go[b].GetComponent<ArmaController>().actualArma = new Bazooka();
                break;

            case 2:
                go[b].GetComponent<ArmaController>().actualArma = new Bazooka();
                break;

            case 3:
                go[b].GetComponent<ArmaController>().actualArma = new Bazooka();
                break;

            case 4:
                go[b].GetComponent<ArmaController>().actualArma = new Bazooka();
                break;

            default:
                break;
        }
        return go[b];
    }

    void ControlarQueda()
    {
        if (goAtual != null)
        {
            Vector3 valor = goAtual.transform.position;
            if (goAtual.transform.position.y > 0.5)
            {
                goAtual.transform.position = new Vector3(valor.x, valor.y -= Time.deltaTime * velocidadeQueda, valor.z);
                goAtual.transform.Rotate(0, Mathf.PingPong(Time.time, 2) - 1, 0);
            }
            else
            {
                InstanciarRandom();
            }
        }
    }

    void InstanciarRandom()
    {
        int b = Random.Range(0, go.Length);
        if (b != go.Length - 1)
            Instantiate(go[b], goAtual.transform.position, Quaternion.identity);
        else
            Instantiate(InstanciarArma(b), goAtual.transform.position, Quaternion.identity);

        Destroy(goAtual);
        goAtual = null;
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
