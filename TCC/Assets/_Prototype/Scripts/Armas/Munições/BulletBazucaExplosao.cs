﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBazucaExplosao : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletBazuca;
    void Start()
    {
        Destroy(gameObject, 1);
    }

    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * 5, transform.localScale.y + Time.deltaTime * 5, transform.localScale.z + Time.deltaTime * 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().ReceiveDamage(bulletBazuca.GetComponent<BulletBazuca>().damage / (int)transform.localScale.x, other.gameObject.GetComponent<PlayerController>());

        }
    }
}
