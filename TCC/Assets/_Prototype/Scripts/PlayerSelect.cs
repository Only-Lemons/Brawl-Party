using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSelect : MonoBehaviour, Inputs.IPlayerActions
{
    public List<Color> playerColor = new List<Color>();
    public Material playerMaterial;
    public Color atualColor;
    public bool isConfirmed = false;
    public PlayerController PlayerGame;
    Vector3 certo = new Vector3(1, 1, 1);
    Vector3 errado = new Vector3(.7f, .7f, .7f);



    private void Awake()
    {
      //  playerMaterial = transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material;

    }
    private void Start()
    {
        atualColor = playerColor[0];
        playerColor.Remove(atualColor);
        playerColor.Add(atualColor);
    }
    void Update()
    {
        if (!isConfirmed)
        {

           // playerMaterial.color = atualColor;
           // transform.GetChild(1).localScale = errado;
        }
        else
        {
           // transform.GetChild(1).localScale = certo;
        }
        //playerBackround.color = Color.red;
    }

    #region InputSystem Events
    public void OnUP(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            atualColor = playerColor[0];
            playerColor.Remove(atualColor);
            playerColor.Add(atualColor);
        }

    }
    public void OnConfirmed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isConfirmed)
                isConfirmed = false;
            else
                isConfirmed = true;
        }
    }


    public void OnAdd(InputAction.CallbackContext context)
    {

    }


    public void OnMove(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnMove(context);
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnLook(context);
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnFire(context);
    }


    public void OnInsert(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnInsert(context);
    }


    public void OnSwitch(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnSwitch(context);
    }


    public void OnAim(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnAim(context);
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        Debug.Log("Show");
        GetComponentInChildren<PlayerController>().OnAction(context);
    }

    public void OnR(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnR(context);
    }

    public void OnL(InputAction.CallbackContext context)
    {
        GetComponentInChildren<PlayerController>().OnL(context);
    }
    #endregion
}
