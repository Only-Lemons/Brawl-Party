using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y <= 0.5)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }
}
