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

    private void Awake()
    {
        playerMaterial = transform.GetChild(1).GetChild(1).GetComponent<Renderer>().material;

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

            playerMaterial.color = atualColor;

        }
        else;
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
    public void OnAdd(InputAction.CallbackContext context) { }
    public void OnMove(InputAction.CallbackContext context) { }
    public void OnLook(InputAction.CallbackContext context) { }
    public void OnFire(InputAction.CallbackContext context) { }
    public void OnInsert(InputAction.CallbackContext context) { }
    public void OnSwitch(InputAction.CallbackContext context) { }
    public void OnAim(InputAction.CallbackContext context) { }

    public void OnAction(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnR(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnL(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
