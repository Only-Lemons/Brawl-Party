using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShake : MonoBehaviour
{

    public void Shake()
    {
        CameraShaker.Instance.ShakeOnce(1f, 5f, .1f, 1f);
    }

}
