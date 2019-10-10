using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FirstScene : MonoBehaviour
{
    public Text textPress;
    float actualTime = 1.8f;
    float timeRun = 0;
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        CrossTextColor();
        PiscarLetreiro();
    }

    private void CrossTextColor()
    {
        
    }

    void PiscarLetreiro()
    {
        timeRun += Time.deltaTime;
        if (timeRun >= actualTime)
        {
            StopAllCoroutines();
            StartCoroutine(IEPiscarLetreiro());
            timeRun = 0;
        }
    }

    IEnumerator IEPiscarLetreiro()
    {
        textPress.gameObject.SetActive(false);
        yield return new WaitForSeconds(actualTime/2);
        textPress.gameObject.SetActive(true);
    }
}
