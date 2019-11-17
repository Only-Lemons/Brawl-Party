using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoConfig : MonoBehaviour
{
    private int qindex = 1;
    public string[] qualityTxt;
    public Text textQuality;

    private void Start()
    {
        textQuality.text = qualityTxt[qindex];
    }

    public void QualityChangeUp()
    {
        if (qindex < 5)
            qindex++;

        QualitySettings.SetQualityLevel(qindex);
        textQuality.text = qualityTxt[qindex + 1];
    }
    public void QualityChangeDown()
    {
        if (qindex > 2)
            qindex--;
        QualitySettings.SetQualityLevel(qindex);
        textQuality.text = qualityTxt[qindex];
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
