using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammer : MonoBehaviour
{
    bool canBrick = false;
    bool canUp = false;
    Vector3 posicaoInicial, posicaoFinal;

    void Start()
    {
        canUp = false;
        posicaoInicial = transform.position;
        posicaoFinal = new Vector3(transform.position.x, -5f, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            other.GetComponent<PlayerController>().ReceiveDamage(10000000, null);
        }
    }
    private void Update()
    {
        if (canBrick)
        {
            this.transform.position = Vector3.Lerp(transform.position, posicaoFinal, 4f * Time.deltaTime);
        }

        if (transform.position.magnitude <= posicaoFinal.magnitude - 1)
            canUp = true;

        if (canUp == true)
        {
            transform.position = Vector3.Lerp(transform.position, posicaoInicial, 1f * Time.deltaTime);
            if (transform.position == posicaoInicial)
                canUp = false;
        }
    }
    public IEnumerator brickHammer()
    {
        for (int i = 0; i < 6; i++)
        {
            transform.position = new Vector3(posicaoInicial.x - 0.2f, posicaoInicial.y, posicaoInicial.z);
            yield return new WaitForSeconds(0.1f);
            transform.position = new Vector3(posicaoInicial.x + 0.2f, posicaoInicial.y, posicaoInicial.z);
            yield return new WaitForSeconds(0.1f);
        }

        //yield return new WaitForSeconds(2f);
        canBrick = true;
        StartCoroutine(backHammer());


    }
    IEnumerator backHammer()
    {
        yield return new WaitForSeconds(1f);
        canUp = true;
        yield return new WaitForSeconds(2f);
        canBrick = false;
        // hammers[i].GetComponent<Animator>().SetTrigger("estate");



    }
}
