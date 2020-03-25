using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    [SerializeField] float resourceAmount;


    public bool takeResource()
    {
        resourceAmount += 10;

        return resourceAmount >= 100;
    }


}
