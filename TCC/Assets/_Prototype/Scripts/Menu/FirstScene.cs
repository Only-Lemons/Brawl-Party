using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FirstScene : MonoBehaviour
{
    public Text textPress;
    float _actualTime = 1.8f;
    float _timeRun = 0;

    void Update()
    {
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
        PiscarLetreiro();
    }

    void CrossTextColor()
    {
        
    }
    void PiscarLetreiro()
    {
        _timeRun += Time.deltaTime;
        if (_timeRun >= _actualTime)
        {
            StopAllCoroutines();
            StartCoroutine(IEPiscarLetreiro());
            _timeRun = 0;
        }
    }

    IEnumerator IEPiscarLetreiro()
    {
        textPress.gameObject.SetActive(false);
        yield return new WaitForSeconds(_actualTime/2);
        textPress.gameObject.SetActive(true);
    }
}
