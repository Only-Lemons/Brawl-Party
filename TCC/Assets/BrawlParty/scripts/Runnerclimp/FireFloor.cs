using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFloor : MonoBehaviour
{
    //private void OnTriggerStay(Collider other)
    //{
    //    //if (other.gameObject.transform.position.y < transform.position.y)
    //    {
    //        Destroy(other.gameObject);
    //    }
    //}

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
    }
}
