using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    public GameObject quitPanel;


    private void Update()
    {
        confirmQuit();
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("01_MainMenu");
    }

    public void sceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void confirmQuit()
    {
        if (Input.GetKeyDown(KeyCode.B) && !quitPanel.activeSelf)
            quitPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.X) && quitPanel.activeSelf)
            quitPanel.SetActive(false);
    }


}
