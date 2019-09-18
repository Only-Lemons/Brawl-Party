using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int velocidadeDaBala = 3;
    public Vector3 transformForward;
    public PlayerController player;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transformForward* 400f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject,5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        { 
            other.GetComponent<PlayerController>().ReceiveDamage(damage,player);
      
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Obstaculo")
        {
            Destroy(this.gameObject);
        }

    }
}
