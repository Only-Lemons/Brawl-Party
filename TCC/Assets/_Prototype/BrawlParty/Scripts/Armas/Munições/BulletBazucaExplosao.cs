using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBazucaExplosao : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject bulletBazuca;
    float amplitude = 7;
    void Start()
    {
        bulletBazuca = Resources.Load("Municoes/ProjetilBazuca") as GameObject;
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
            other.GetComponent<PlayerController>().ReceiveDamage(bulletBazuca.GetComponent<BulletBazuca>().damage / ((int)transform.localScale.x + 1), other.gameObject.GetComponent<PlayerController>());

        }
    }
}
