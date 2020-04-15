using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBroken : MonoBehaviour
{
    float jumpForce = 10f;
    void Start()
    {
        gameObject.name = "Platform";
    }

    private void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag == "Stone" || other.gameObject.tag == "Player")
        if (other.relativeVelocity.y <= 0f)
        {
            Rigidbody rb = other.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 velocity = rb.velocity;
                velocity.y = jumpForce;
                rb.velocity = velocity;
            }
        }
    }
}
