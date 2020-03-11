using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float gravityScale, planetRadius, gravityMinRange, gravityMaxRange;
    public GameObject planet, minRS, maxRS;

    public Transform gravityPoint;

    private void Start()
    {
        gravityPoint = this.transform;
    }


    void OnTriggerStay(Collider obj)
    {
        float gravitationalPower = gravityScale / planetRadius;

        //float dist = Vector3.Distance(obj.transform.position, transform.position);
        //if (dist > (planetRadius + gravityMinRange))
        //{
        //    float min = planetRadius + gravityMinRange + 0.5f;
        //    gravitationalPower = gravitationalPower * (((min + gravityMaxRange) - dist) / gravityMaxRange);
        //}

        Vector3 dir = (transform.position - obj.transform.position) * gravityScale;
        obj.GetComponent<Rigidbody>().AddForce(dir);

        if (obj.CompareTag("Player"))
        {
            obj.transform.up += Vector3.MoveTowards(obj.transform.up, -dir, gravityScale * Time.deltaTime * 5f);
        }
    }
}