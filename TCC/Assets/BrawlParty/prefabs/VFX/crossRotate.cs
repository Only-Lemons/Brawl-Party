using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossRotate : MonoBehaviour
{
    [SerializeField]
    private GameObject go;

    void Update()
    {
        go.transform.Rotate(0, 2, 0);
    }
}
