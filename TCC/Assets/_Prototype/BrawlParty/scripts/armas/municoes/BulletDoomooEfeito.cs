using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDoomooEfeito : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject bulletDoomoo;
    float velocidadeExpansao = 16;
    float tamanhoMax = 36;
    bool entrou = false;

    float cont;

    void Start()
    {
        entrou = false;
        bulletDoomoo = Resources.Load("Municoes/ProjetilDoomoo") as GameObject;
        Destroy(gameObject, 10);
    }

    void Update()
    {
        if (transform.localScale.magnitude < tamanhoMax)
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * velocidadeExpansao, transform.localScale.y + Time.deltaTime * velocidadeExpansao, transform.localScale.z + Time.deltaTime * velocidadeExpansao);

        if (entrou)
        {
            cont += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            entrou = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (cont > 0.5f)
            {
                other.GetComponent<PlayerController>().ReceiveDamage(bulletDoomoo.GetComponent<BulletDoomoo>().damage, other.gameObject.GetComponent<PlayerController>());
                cont = 0;
            }

            other.transform.position = Vector3.Lerp(other.transform.position,new Vector3( transform.position.x, transform.position.y, transform.position.z), Time.deltaTime);
        }
    }
}
