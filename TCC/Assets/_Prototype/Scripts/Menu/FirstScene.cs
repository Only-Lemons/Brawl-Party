using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstScene : MonoBehaviour
{
    public Text textPress;
    public float _actualTime = 1.8f;
    float _timeRun = 0;

    bool subir;

    public Image logo;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }

        ScalarLetreiro();
        CrossTextColor();
    }

    void CrossTextColor()
    {
        textPress.color = new Color(1/_timeRun, _timeRun, 0);
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
        yield return new WaitForSeconds(_actualTime / 2);
        textPress.gameObject.SetActive(true);
    }

    void ScalarLetreiro()
    {
        if (_timeRun >= 1)
        {
            subir = false;
        }
        else if (_timeRun <= 0.3f)
            subir = true;

        if (subir)
        {
            _timeRun += Time.deltaTime;
            textPress.gameObject.transform.localScale = new Vector3(_timeRun + 1, _timeRun + 1, _timeRun + 1);
        }
        if (!subir)
        {
            _timeRun -= Time.deltaTime;
            textPress.gameObject.transform.localScale = new Vector3(_timeRun + 1, _timeRun + 1, _timeRun + 1);
        }
    }

}
