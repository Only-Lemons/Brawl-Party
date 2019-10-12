using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAU : MonoBehaviour
{
    void Update()
    {
        ControlarQueda();
    }

    void ControlarQueda()
    {
        Vector3 valor = transform.position;
        if (transform.position.y > 0.5)
        {
            transform.position = new Vector3(valor.x, valor.y -= Time.deltaTime * TesteInstancias.testeI.velocidadeQueda, valor.z);
            transform.transform.Rotate(0, Mathf.PingPong(Time.time, 2) - 1, 0);
        }
        else
        {
            InstanciarRandom();
            Destroy(this.gameObject);
        }
    }

    public void InstanciarRandom()
    {
        int b = Random.Range(0, TesteInstancias.testeI.go.Length);
        if (b != TesteInstancias.testeI.go.Length - 1)
            Instantiate(TesteInstancias.testeI.go[b], this.transform.position, Quaternion.identity, GameObject.Find("Manager").transform);
        else
            Instantiate(TesteInstancias.testeI.InstanciarArma(b), this.transform.position, Quaternion.identity, GameObject.Find("Manager").transform);
    }
}
