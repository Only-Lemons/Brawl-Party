using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCongelante : MonoBehaviour
{
    public int damage;
    public int velocidadeDaBala = 3;
    public Vector3 transformForward;
    public PlayerController player;

    bool iniciar;
    Vector3 posicao;
    private float cont;

    GameObject geloEfeito;
    GameObject go;

    void Start()
    {
        geloEfeito = Resources.Load("Municoes/Efeitos/CuboGelo") as GameObject;
        iniciar = false;
        cont = 0;
        GetComponent<Rigidbody>().AddForce(transformForward * 600f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject, 5f);
    }

    private void Update()
    {
        if (iniciar)
            Contador();
    }

    void Contador()
    {
        cont += Time.deltaTime;

        if (cont > 1)
        {
            Destroy(go);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(player);
            other.GetComponent<PlayerController>().ReceiveDamage(damage, player);

            if (iniciar == false)
            {
                posicao = other.transform.position;
                go = Instantiate(geloEfeito, posicao, Quaternion.identity, other.transform);
            }

            iniciar = true;
        }
        else if (other.gameObject.tag == "Obstaculo")
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.transform.position = posicao;
            other.transform.position = posicao;

        }
    }
}
