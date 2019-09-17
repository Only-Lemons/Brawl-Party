using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int velocidadeDaBala = 3;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 200f * velocidadeDaBala);
        Destroy(this.gameObject,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        { 
            other.GetComponentInParent<PlayerController>().ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Obstaculo")
        {
            Destroy(this.gameObject);
        }

    }
}
