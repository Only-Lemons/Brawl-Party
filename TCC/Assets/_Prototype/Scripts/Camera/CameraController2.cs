using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CameraController2 : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public float posicaoEmZ = -6;

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
        if (targ != null)
        {
            ControleFoco();
            ControleZoom();
        }
    }

    void ControleFoco()
    {
        cam.transform.position = new Vector3(MediaDistancia().x, cam.transform.position.y, MediaDistancia().z + (posicaoEmZ * -1));
        cam.transform.LookAt(MediaDistancia());
    }

    void ControleZoom()
    {
        //cam.transform.position = ZoomDistancia();
        cam.fieldOfView = ZoomDistanciaFOV();
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

    Vector3 ZoomDistanciaPOS()
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
        if (max > 20)
            z = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, 40, Time.deltaTime), cam.transform.position.z);
        if (max <= 20 && max>=5)
            z = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, 20, Time.deltaTime), cam.transform.position.z);
        if (max < 5)
            z = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, 10, Time.deltaTime), cam.transform.position.z);
        return z;
    }

    float ZoomDistanciaFOV()
    {
        float z = 0;
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
        if (max > 20)
            z = Mathf.Lerp(cam.fieldOfView, 65, Time.deltaTime);
        if (max <= 20 && max>=5)
            z = Mathf.Lerp(cam.fieldOfView, 45, Time.deltaTime);
        if (max < 5)
            z = Mathf.Lerp(cam.fieldOfView, 20, Time.deltaTime);
        return z;
    }
}