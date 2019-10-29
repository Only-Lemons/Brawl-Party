using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QualitySetting : MonoBehaviour
{
    public Toggle antiAlising;
    public Dropdown ShadowQ, 
                    Resolution;

    private void Start()
    {
        if (QualitySettings.antiAliasing > 1)
        {
            antiAlising.isOn = true;
        }
        switch (QualitySettings.shadowResolution)
        {
            case ShadowResolution.High:
                ShadowQ.value = 2;
                break;
            case ShadowResolution.Medium:
                ShadowQ.value = 1;
                break;
            case ShadowResolution.Low:
                ShadowQ.value = 0;
                break;

        }
        switch (QualitySettings.GetQualityLevel())
        {
            case 5:
                Resolution.value = 2;
                break;
            case 2:
                Resolution.value = 1;
                break;
            case 1:
                Resolution.value = 0;
                break;
            default:
                Resolution.value = 2;
                break;
        }
    }
    public void ChangeAntiAliasing(bool value)
    {
        if (value)
            QualitySettings.antiAliasing = 2;
        else
            QualitySettings.antiAliasing = 0;
    }
    public void ChangeShadowResolution(int Index)
    {
        switch (Index)
        {
            case 2 :
              QualitySettings.shadowResolution = ShadowResolution.High;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                break;
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                break;

        }
    }
    public void ChangeResolution(int Index)
    {
        switch (Index)
        {
            case 0:
              QualitySettings.SetQualityLevel(0);
                break;
            case 1:
                QualitySettings.SetQualityLevel(3);
                break;
            case 2:
                QualitySettings.SetQualityLevel(5);
                break;
        }
    }
}
