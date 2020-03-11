using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float gravityScale = 12f, planetRadius = 2f, gravityMinRange = 1f, gravityMaxRange = 2f;
    public GameObject planet, minRS, maxRS;

    public Transform pos;



    void OnTriggerStay(Collider obj)
    {
        //    float gravitationalPower = gravityScale / planetRadius;
        //    float dist = Vector3.Distance(obj.transform.position, transform.position);

        //    //if (dist > (planetRadius + gravityMinRange))
        //    //{
        //    //    float min = planetRadius + gravityMinRange + 0.5f;
        //    //    gravitationalPower = gravitationalPower * (((min + gravityMaxRange) - dist) / gravityMaxRange);
        //    //}

        //    Vector3 dir = (transform.position - obj.transform.position) * gravityScale;
        //    obj.GetComponent<Rigidbody>().AddForce(dir);

        //    if (obj.CompareTag("Player"))
        //    {
        //        obj.transform.up = Vector3.MoveTowards(obj.transform.up, -dir, gravitationalPower * Time.deltaTime * 5f);
        //    }

        
    }



}
