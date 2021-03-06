﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour
{
    Vector3 position;
    public float speed = 2f;
    private void Start()
    {
        position = this.transform.position;
        Destroy(this.gameObject, 5f);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            this.GetComponent<ParticlePlayer>().Play(5f);
            GameController.singleton.gameMode.PointRule(other.GetComponent<Basket>().player);
            Destroy(this.gameObject);
        }
   
    }
    private void Update()
    {
        // this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(position.x, 0, position.z), 1 * Time.deltaTime);
        this.transform.position -= Vector3.up*Time.deltaTime*speed;
    }
}
