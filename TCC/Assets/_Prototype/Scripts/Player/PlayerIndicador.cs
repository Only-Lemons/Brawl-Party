using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicador : MonoBehaviour
{
    public Text playerIndicator;

    void Update()
    {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        playerIndicator.transform.position = namePos;
    }
}
