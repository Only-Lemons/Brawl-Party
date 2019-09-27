using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FirstScene : MonoBehaviour
{
    public Text textPress;
    float actualTime = 1;
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        CrossTextColor();
    }

    private void CrossTextColor()
    {
       
    }
}
