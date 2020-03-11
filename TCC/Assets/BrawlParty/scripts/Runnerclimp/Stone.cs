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
        //randomPosStone =  Random.Range(-6f, 6f);
        //transform.position = new Vector3(randomPosStone, transform.position.y, transform.position.z);
        Destroy(this.gameObject, 3);
        ramdom = Random.Range(40f, 80f);
        GetComponent<Rigidbody>().AddForce(Vector3.down*100, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(ramdom*Time.deltaTime, ramdom*Time.deltaTime, ramdom*Time.deltaTime);
    }
}
