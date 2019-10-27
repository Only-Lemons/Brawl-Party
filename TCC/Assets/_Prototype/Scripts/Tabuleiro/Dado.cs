using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dado : MonoBehaviour
{
    public static Dado dadoControl;

    public bool dadoNoAr = false;
    public int dadoValor;

    public bool dadoParado = true;
    float random = 0;
    Vector3 posicaoInicial;
    public void Jogar()
    {
        dadoNoAr = true;
        dadoParado = false;
        TabuleiroControl.tabControl.travarTudo = false;

        random = Random.Range(0, 360);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 10, ForceMode.Impulse);
        
    }

    private void Start()
    {
        if (dadoControl == null)
            dadoControl = this;

        dadoNoAr = false;
        dadoParado = true;

        posicaoInicial = transform.position;
    }

    private void FixedUpdate()
    {
        if(dadoNoAr)
        {
            transform.Rotate(random * Time.deltaTime, random * Time.deltaTime, random * Time.deltaTime);
        }

        VoltarPraOrigem();
    }

    void VoltarPraOrigem()
    {
        if(!dadoNoAr)
        {
            transform.position = Vector3.Lerp(transform.position, posicaoInicial, Time.deltaTime);
            if(Vector3.Distance(transform.position, posicaoInicial) < 0.1f)
            {
                dadoParado = true;

                TabuleiroControl.tabControl.PodeIr();

            }
        }
    }
    
}
