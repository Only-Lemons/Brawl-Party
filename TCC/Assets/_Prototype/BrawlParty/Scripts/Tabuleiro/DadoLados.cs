using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadoLados : MonoBehaviour
{
    public int valor;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BaseDado")
        {
            Dado.dadoControl.dadoNoAr = false;
            Dado.dadoControl.dadoValor = valor;
        }
    }
}
