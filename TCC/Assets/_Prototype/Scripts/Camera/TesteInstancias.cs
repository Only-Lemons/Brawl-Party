using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteInstancias : MonoBehaviour
{
    public GameObject[] go;
    public int velocidadeQueda = 10;
    public Vector2 limiteParaInstanciar = new Vector2(15,15);
    public GameObject cxPadrao;

    GameObject goAtual;
    Vector3 pontoRef;
    bool podeContinuar;
    float timer;
    public int tempoRespawn = 5;
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

    void InstanciarCaixa()
    {
        int x = (int)Random.Range(-limiteParaInstanciar.x, limiteParaInstanciar.x+1);
        int z = (int)Random.Range(-limiteParaInstanciar.y, limiteParaInstanciar.y+1);
        pontoRef = new Vector3(x, 20, z);

        if (goAtual == null)
        {  
            goAtual = Instantiate(cxPadrao, pontoRef, Quaternion.identity);
        }
    }

    void Instanciar()
    {
        timer += Time.deltaTime;
        if (timer >= tempoRespawn)
        {
            timer = 0;
            InstanciarCaixa();
        }
    }

    public GameObject InstanciarArma(int b)
   {
       int rnd = Random.Range(0,5);
       switch(rnd)
       {
            case 0:
                go[b].GetComponent<ArmaController>().actualArma = new Pistol() ;
                break;
            case 1:
             go[b].GetComponent<ArmaController>().actualArma = new Pistol();
                break;
  
            case 2:
             go[b].GetComponent<ArmaController>().actualArma = new Shotgun();
                break;
  
            case 3:
             go[b].GetComponent<ArmaController>().actualArma = new Shotgun();
                break;
  
            case 4:
             go[b].GetComponent<ArmaController>().actualArma = new Pistol();
                break;

            default:
                break;
       }
       return go[b];
   }

    void ControlarQueda()
    {
        if(goAtual != null)
        {
            Vector3 valor = goAtual.transform.position;
            if (goAtual.transform.position.y > 0.5)
            {
                goAtual.transform.position = new Vector3(valor.x, valor.y -= Time.deltaTime*velocidadeQueda, valor.z);
                goAtual.transform.Rotate(0,Mathf.PingPong(Time.time, 2)-1,0);
                
            }
            else
            {
                InstanciarRandom();
            }
        }
    }

    void InstanciarRandom()
    {
        int b = Random.Range(3, go.Length);
        if (b != go.Length - 1)
            Instantiate(go[b], goAtual.transform.position, Quaternion.identity);
        else
            Instantiate(InstanciarArma(b), goAtual.transform.position, Quaternion.identity);

        Destroy(goAtual);
        goAtual = null;
    }

}
