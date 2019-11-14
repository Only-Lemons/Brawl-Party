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
        if (sceneName == "(6) selectMode")
            GameManager.Instance.quantGames = 0;

        SceneManager.LoadScene(sceneName);
    }

    public void OnReturn()
    {
        SceneManager.LoadScene("(1)MenuInicial");
    }

}
