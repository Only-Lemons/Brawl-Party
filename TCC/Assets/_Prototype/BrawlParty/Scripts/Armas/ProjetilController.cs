using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjetilController : MonoBehaviour
{
   void Start()
   {
      GetComponent<Rigidbody>().AddForce(Vector3.forward * 100f);
   }


}

