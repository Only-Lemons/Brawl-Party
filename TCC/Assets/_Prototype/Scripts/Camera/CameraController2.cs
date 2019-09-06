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
        CameraMan();
    }

    void ControleFoco()
    {
        cam.transform.position = new Vector3(MediaDistancia().x, cam.transform.position.y, MediaDistancia().z);
    }

    void CameraMan()
    {
        cam.transform.position = ZoomDistancia();
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

    Vector3 ZoomDistancia()
    {
        Vector3 z = Vector3.zero;
        float max = 0;
        for (int i = 0; i < targ.Count; i++)
        {
            for (int j = 0; j < targ.Count; j++)
            {
                if(i != j)
                {
                    if (Vector3.Distance(targ[i].transform.position, targ[j].transform.position) > max)
                    {
                        max = Vector3.Distance(targ[i].transform.position, targ[j].transform.position);
                    }
                }
            }
            
        }
        if (max > 30)
            z = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, 30, Time.deltaTime), cam.transform.position.z);
        if (max <= 30 && max>=10)
            z = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, 20, Time.deltaTime), cam.transform.position.z);
        if (max < 10)
            z = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, 10, Time.deltaTime), cam.transform.position.z);
        return z;
    }

}