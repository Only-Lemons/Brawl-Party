using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChangeScene : MonoBehaviour,Inputs.IUIActions
{

    public GameObject quitPanel;
    public Inputs controls;
    void Awake()
    {
        controls = new Inputs();
    }


    private void Update()
    {
        confirmQuit();
       // if (Input.GetKeyDown(KeyCode.R))
        //    SceneManager.LoadScene("01_MainMenu");
    }

    public void sceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void voltarMenu()
    {
         
    }


    public void confirmQuit()
    {
        if (Input.GetKeyDown(KeyCode.B) && !quitPanel.activeSelf)
            quitPanel.SetActive(true);

        if (Input.GetKeyDown(KeyCode.X) && quitPanel.activeSelf)
            quitPanel.SetActive(false);
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            SceneManager.LoadScene("(1)MenuInicial");
        }
    
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceSelect(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
