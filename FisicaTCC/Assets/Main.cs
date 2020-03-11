using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        Vec3 v1 = new Vec3(1, 3, 2);
        Vec3 v2 = new Vec3(1, 2, 1);
        Vec3 v = v1 / v2;

        Debug.Log(v.ToString());
    }

}
