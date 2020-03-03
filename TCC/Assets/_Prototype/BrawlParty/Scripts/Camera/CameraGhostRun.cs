using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class CameraGhostRun : MonoBehaviour
{
    Camera cam;
    public Vector3 posCam;
    List<PlayerController> targ;

    float sizeOrtog; // modo de câmera ortográfica

    void Start()
    {
        targ = GameController.singleton.playerManager.playersControllers;
        cam = Camera.main;

        posCam = cam.transform.position;
        cam.orthographicSize = 8;

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
        }
    }

    void ControleFoco()
    {
        cam.transform.position = new Vector3(Mathf.Lerp(posCam.x, MediaDistancia().x, 2), posCam.y, Mathf.Lerp(posCam.z, MediaDistancia().z - 19, 2));
    }

    void ControleZoom()
    {
        cam.transform.position = ZoomDistanciaPOS(); //Distancia por posição
        //cam.fieldOfView = ZoomDistanciaFOV(); //Distancia por fov
        //cam.orthographicSize = ZoomDistanciaORTO(); //Distancia por size - MODO DE CÂMERA ORTOGRÁFICA
    }
    Vector3 MediaDistancia()
    {
        Vector3 m = Vector3.zero;
        for (int i = 0; i < targ.Count; i++)
        {
            m += targ[i].transform.position;
        }

        return m / (targ.Count + 20);
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

        z = new Vector3(cam.transform.position.x, Mathf.Lerp(cam.transform.position.y, 20, Time.deltaTime), cam.transform.position.z);
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

        z = Mathf.Lerp(cam.fieldOfView, max + 30, Time.deltaTime * 4);
        return z;
    }

    float ZoomDistanciaORTO()
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

        z = Mathf.Lerp(cam.orthographicSize, (max / 8) + 8, Time.deltaTime * 4);
        return z;
    }

}