using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void ChangeSceneWithLoad(int scene)
    {
       
        GameManager.Instance.nextLevel = scene;
        SceneManager.LoadScene(3);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
   
    
}
