using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiro : MonoBehaviour
{
    public int damage;

    void Update()
    {
        transform.position += transform.forward*Time.deltaTime*10;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponentInParent<PlayerController>().ReceiveDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
