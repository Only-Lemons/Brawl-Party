using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteInstancias : MonoBehaviour
{
    public GameObject[] go;
    public int velocidadeQueda = 3;

    GameObject goAtual;
    Vector3 pontoRef;
    bool podeContinuar;
    float timer;
    public int tempoRespawn;
    void Start()
    {
        goAtual = null;
        podeContinuar = true;
        timer = 0;
        pontoRef = Vector3.zero;
    }

    void Update()
    {
        Instanciar();
        ControlarQueda();
    }

    void InstanciarRandom()
    {
        int x = Random.Range(-15, 16);
        int z = Random.Range(-15, 16);
        pontoRef = new Vector3(x, 20, z);

        if (goAtual == null)
        {
            goAtual = Instantiate(go[Random.Range(0, go.Length)], pontoRef, Quaternion.identity);
        }
    }

    void Instanciar()
    {
        timer += Time.deltaTime;
        if (timer >= tempoRespawn)
        {
            timer = 0;
            InstanciarRandom();
        }
    }

    void ControlarQueda()
    {
        if(goAtual != null)
        {
            Vector3 valor = goAtual.transform.position;
            if (goAtual.transform.position.y > 0.5)
            {
                goAtual.transform.position = new Vector3(valor.x, valor.y -= Time.deltaTime*velocidadeQueda, valor.z);
            }
            else
            {
                goAtual = null;
            }
        }
    }

}
