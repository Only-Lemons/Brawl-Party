using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShake : MonoBehaviour
{

    public void Shake()
    {
        CameraShaker.Instance.ShakeOnce(2f, 4f, .1f, 1f);
    }

}
