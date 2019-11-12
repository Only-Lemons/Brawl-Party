using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class ChangeScene : MonoBehaviour
{
    

    public void sceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnReturn()
    {
        SceneManager.LoadScene("(1)MenuInicial");
    }

}
