using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController2 : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    GameObject[] targ;

    void Start()
    {
        cam = Camera.main;
        targ = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowerZoom();
    }

    void FollowerZoom()
    {
        cam.transform.LookAt((targ[0].transform.position + targ[1].transform.position) / targ.Length);
    }

    Vector3 PlayerLenght(GameObject player)
    {
        return player.transform.position;
    }
}
