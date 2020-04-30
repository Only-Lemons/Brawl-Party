using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField]float resourceAmount= 100;


    private void Update()
    {
        if (resourceAmount <= 0)
            Destroy(this.gameObject);
    }



    public float takeResource()
    {
        resourceAmount -= 10;
        return 10;
    }



}
