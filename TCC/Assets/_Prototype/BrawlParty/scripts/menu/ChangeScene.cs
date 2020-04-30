using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

public class ChangeScene : MonoBehaviour
{
    public string menuIncial;
    public string selectMode;

    public void sceneChange(string sceneName)
    {
        if (sceneName == selectMode)
            GameManager.Instance.quantGames = 0;

        SceneManager.LoadScene(sceneName);
    }

    public void OnReturn()
    {
        SceneManager.LoadScene(menuIncial);
    }

}
