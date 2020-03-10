using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 5);
    }

}
