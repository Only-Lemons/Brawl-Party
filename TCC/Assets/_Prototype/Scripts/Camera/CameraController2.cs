using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CameraController2 : MonoBehaviour
{
    public static CameraController2 Singleton;
    Camera cam;
    public float posicaoEmZ = 8;
    public float alturaCam = 25;
    List<PlayerController> targ;

    void Start()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
            Destroy(gameObject);

        targ = GameController.singleton.playerManager.playersControllers;
        cam = Camera.main;
        cam.transform.position = new Vector3(0, alturaCam, -10);
      

    }
    public void GetTargets()
    {
        targ.Clear();
        targ = GameController.singleton.playerManager.playersControllers;

    }


    void LateUpdate()
    {
        if (targ.Count > 0)
        {
            ControleFoco();
            ControleZoom();
        }
    }

    void ControleFoco()
    {
        cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, MediaDistancia().x, 2), cam.transform.position.y, Mathf.Lerp(cam.transform.position.z, MediaDistancia().z + (posicaoEmZ * -1), 2));
        cam.transform.LookAt(Vector3.Lerp(transform.position, MediaDistancia(), Time.deltaTime));
    }

    void ControleZoom()
    {
        //cam.transform.position = ZoomDistanciaPOS(); //Distancia por posição
        cam.fieldOfView = ZoomDistanciaFOV(); //Distancia por fov
    }
    Vector3 MediaDistancia()
    {
        Vector3 m = Vector3.zero;
        for (int i = 0; i < targ.Count; i++)
        {
            m += targ[i].transform.position;
        }

        return m / (targ.Count +1);
    }

    Vector3 ZoomDistanciaPOS()
    {
        Vector3 z = Vector3.zero;
        float max = 0;
        for (int i = 0; i < targ.Count; i++)
        {
            for (int j = 0; j < targ.Count; j++)
            {
                if (i != j)
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
        if (max <= 20 && max >= 5)
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
                if (i != j)
                {
                    if (Vector3.Distance(targ[i].transform.position, targ[j].transform.position) > max)
                    {
                        max = Vector3.Distance(targ[i].transform.position, targ[j].transform.position);
                        
                    }
                }
            }
        }
        //if (max > 12)
        //    z = Mathf.Lerp(cam.fieldOfView, 65, Time.deltaTime);
        //if (max <= 12)
        //    z = Mathf.Lerp(cam.fieldOfView, 50, Time.deltaTime / 1.2f);
        z = Mathf.Lerp(cam.fieldOfView, max + 30, Time.deltaTime*4);
        return z;
    }
}