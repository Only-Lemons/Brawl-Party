using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGranadeExplosao : MonoBehaviour
{
    // Start is called before the first frame update
    float amplitude = 3;
    GameObject bulletGranada;
    void Start()
    {
        bulletGranada = Resources.Load("Municoes/ProjetilGranada") as GameObject;
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * amplitude, transform.localScale.y + Time.deltaTime * amplitude, transform.localScale.z + Time.deltaTime * amplitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().ReceiveDamage(bulletGranada.GetComponent<BulletGranade>().damage / ((int)transform.localScale.x + 1), other.gameObject.GetComponent<PlayerController>());

        }
    }
}
