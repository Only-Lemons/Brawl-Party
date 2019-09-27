using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Slider sliderLoad;
    void Awake()
    { 
        StartCoroutine(LoadAsynchronously(GameManager.Instance.nextLevel));
    }
    IEnumerator LoadAsynchronously(int scene)
    {
        yield return new WaitForSeconds(3);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            sliderLoad.value = operation.progress / 9;
            yield return  null;
            
        }
    }
}
