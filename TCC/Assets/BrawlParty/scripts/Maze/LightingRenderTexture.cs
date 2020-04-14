using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LightingRenderTexture : MonoBehaviour
{
    [SerializeField]
    Material ScreenMat;
    [SerializeField]
    Camera lightingCam;

    RenderTexture rt;
    private void Start()
    {
        rt = new RenderTexture(Screen.width, Screen.height, 24);
        lightingCam.targetTexture = rt;
        ScreenMat.SetTexture("_RenderTexture", rt);
    }
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        if (ScreenMat != null)
            Graphics.Blit(src, dst, ScreenMat);
    }


}