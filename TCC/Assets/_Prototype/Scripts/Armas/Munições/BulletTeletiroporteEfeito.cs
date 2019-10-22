using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTeletiroporteEfeito : MonoBehaviour
{
    int meuNum;
    GameObject[] qtd;

    void Start()
    {
        qtd = GameObject.FindGameObjectsWithTag("Teletiroportador");
        meuNum = qtd.Length;
        Debug.Log("Teleporte " + meuNum);

        Destroy(gameObject, 20);
    }

    void FixedUpdate()
    {
        qtd = GameObject.FindGameObjectsWithTag("Teletiroportador");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 posicaoCorrigida = new Vector3(0, 0, 0);
            int rnd = Random.Range(0, qtd.Length);

            if (qtd.Length > 1)
                if (rnd == meuNum)
                {
                    rnd = Random.Range(0, qtd.Length);
                    if (rnd == meuNum)
                        rnd = Random.Range(0, qtd.Length);
                }

            if (transform.position.x > 0 && transform.position.z > 0)
                posicaoCorrigida = new Vector3(-1, 0, -1);
            else if (transform.position.x < 0 && transform.position.z < 0)
                posicaoCorrigida = new Vector3(1, 0, 1);
            else if (transform.position.x > 0 && transform.position.z < 0)
                posicaoCorrigida = new Vector3(-1, 0, 1);
            else if (transform.position.x < 0 && transform.position.z > 0)
                posicaoCorrigida = new Vector3(1, 0, -1);

            other.transform.position = qtd[rnd].transform.position + posicaoCorrigida;
        }
    }
}
