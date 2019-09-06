using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CameraController2 : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;

    public List<GameObject> targ = new List<GameObject>();
    void Start()
    {
        cam = Camera.main;
        GetTargets();

    }
    void GetTargets()
    {
        targ.Clear();
        GameObject[] aux = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject item in aux)
        {
            targ.Add(item);
        }
    }


    void LateUpdate()
    {
        ControleFoco();

    }

    void ControleFoco()
    {
        cam.transform.LookAt(MediaDistancia());
        CameraMan();

        cam.fieldOfView = ZoomDistancia(targ[0].transform, targ[1].transform, targ[2].transform, targ[3].transform);
        //cam.fieldOfView = ZoomDistancia(targ);
    }

    void CameraMan()
    {

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

    float ZoomDistancia(Transform a, Transform b, Transform c, Transform d)
    {
        float z = 0;
        if (a != null || b != null || c != null || d != null)
        {
            if (Vector3.Distance(a.position, b.position) > 20 || Vector3.Distance(a.position, c.position) > 20 || Vector3.Distance(a.position, d.position) < 10 || Vector3.Distance(b.position, c.position) < 10 || Vector3.Distance(b.position, d.position) > 20 || Vector3.Distance(c.position, d.position) > 20)
            {
                z = Mathf.Lerp(cam.fieldOfView, 90, Time.deltaTime * 5);
            }
            if (Vector3.Distance(a.position, b.position) <= 20 || Vector3.Distance(a.position, c.position) <= 20 || Vector3.Distance(a.position, d.position) <= 20 || Vector3.Distance(b.position, c.position) < 10 || Vector3.Distance(b.position, d.position) < 10 || Vector3.Distance(c.position, d.position) <= 20)
            {
                if (Vector3.Distance(a.position, b.position) >= 10 || Vector3.Distance(a.position, c.position) >= 10 || Vector3.Distance(a.position, d.position) < 10 || Vector3.Distance(b.position, c.position) < 10 || Vector3.Distance(b.position, d.position) >= 10 || Vector3.Distance(c.position, d.position) >= 10)
                {
                    z = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 5);
                }
            }
            if (Vector3.Distance(a.position, b.position) < 10 || Vector3.Distance(a.position, c.position) < 10 || Vector3.Distance(a.position, d.position) < 10 || Vector3.Distance(b.position, c.position) < 10 || Vector3.Distance(b.position, d.position) < 10 || Vector3.Distance(c.position, d.position) < 10)
            {
                z = Mathf.Lerp(cam.fieldOfView, 30, Time.deltaTime * 5);
            }
        }
        return z;
    }
    float ZoomDistancia(List<GameObject> m)
    {
        float z = 0;

        return z;
    }

}