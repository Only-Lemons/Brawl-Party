using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdJB : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.down * 200, ForceMode.Force);
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + Mathf.PingPong(Time.time/2.0f, 0.6f) - 0.3f, transform.position.y, transform.position.z);
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.position -= new Vector3(0, 3, 0);
            //GameController.singleton.gameMode.HitRule(other.GetComponent<PlayerController>());
            this.GetComponent<ParticlePlayer>().Play(.2F);

            Destroy(this.gameObject, .2F);
        }
    }
}
