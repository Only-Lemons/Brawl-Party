using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teste : MonoBehaviour, Inputs.IUIActions
{
    Inputs controls;

    void Awake()
    {
        controls = new Inputs();
    }


    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        GetComponent<Renderer>().material.color = Color.blue;
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnTrackedDeviceSelect(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
