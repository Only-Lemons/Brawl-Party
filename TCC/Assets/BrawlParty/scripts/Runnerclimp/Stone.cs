using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    // Start is called before the first frame update
    float ramdom;
    public float randomPosStone;
    void Start()
    {
        //Destroy(this.gameObject, 3);
        ramdom = Random.Range(40f, 80f);
        GetComponent<Rigidbody>().AddForce(Vector3.down*100, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(ramdom*Time.deltaTime, ramdom*Time.deltaTime, ramdom*Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "Platform")
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.down, ForceMode.Impulse);
            //Destroy(other.gameObject); //temporario
        }
    }
}
