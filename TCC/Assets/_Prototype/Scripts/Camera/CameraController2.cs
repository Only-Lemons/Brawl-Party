using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraController2 : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public List<GameObject> targ;
    void Start()
    {
        //media = GameObject.Find("Media").transform;
        cam = Camera.main;

        //for (int i = 0; i < targ.Count; i++)
        //{
        //    targ[i] = ;
        //}
        //targ.Add( ()as GameObject);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //FollowerZoom();
        FollowZoom();

        if (Input.GetKeyDown(KeyCode.Space))
            targ.Remove(targ[0]);
    }

    //void FollowerZoom()
    //{
    //    cam.transform.LookAt((MediaDistancia()));

    //    if (Vector3.Distance(targ[0].transform.position, targ[1].transform.position) > 20)
    //    {
    //        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 90, Time.deltaTime * 5);
    //    }
    //    if (Vector3.Distance(targ[0].transform.position, targ[1].transform.position) <= 20 && Vector3.Distance(targ[0].transform.position, targ[1].transform.position) >= 10)
    //    {
    //        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 5);
    //    }

    //    if ((Vector3.Distance(targ[0].transform.position, targ[1].transform.position) < 10))
    //    {
    //        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 30, Time.deltaTime * 5);
    //    }
    //}

    void FollowZoom()
    {
        cam.transform.LookAt(MediaDistancia());

        if (Vector3.Distance(targ[0].transform.position, targ[1].transform.position) > 20)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 90, Time.deltaTime * 5);
        }
        if (Vector3.Distance(targ[0].transform.position, targ[1].transform.position) <= 20 && Vector3.Distance(targ[0].transform.position, targ[1].transform.position) >= 10)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 5);
        }

        if ((Vector3.Distance(targ[0].transform.position, targ[1].transform.position) < 10))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 30, Time.deltaTime * 5);
        }
    }

    Vector3 MediaDistancia()
    {
        Vector3 m = Vector3.zero;
        for (int i = 0; i < targ.Count; i++)
        {
            m += targ[i].transform.position;
        }

        return m / targ.Count;
    }
}
