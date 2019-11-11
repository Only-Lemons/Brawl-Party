using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


[System.Serializable]
public struct playerUI
{
    public Color cor;
    public Sprite sprite;
}


public class PlayerSelect : MonoBehaviour, Inputs.IPlayerActions
{

    public List<playerUI> playersUI = new List<playerUI>();
    //public List<Color> playerColor = new List<Color>();
    public Material playerMaterial;
    public Image actualPlayerSprite;
    public Sprite actualPlayerRealSprite;
    //public List<Sprite> playerSprites = new List<Sprite>();
    public Color atualColor;
    public bool isConfirmed = false;
    public PlayerController PlayerGame;
    Vector3 certo = new Vector3(1, 1, 1);
    Vector3 errado = new Vector3(.7f, .7f, .7f);

    private void Start()
    {
        actualPlayerRealSprite = playersUI[0].sprite;
        atualColor = playersUI[0].cor;
        playersUI.Remove(playersUI[0]);
        playerUI aux;
        aux.cor = atualColor;
        aux.sprite = actualPlayerRealSprite;
        playersUI.Add(aux);


    }
    void Update()
    {
        if (!isConfirmed)
        {
            actualPlayerRealSprite = playersUI[0].sprite;
            actualPlayerSprite.sprite = actualPlayerRealSprite;
            atualColor = playersUI[0].cor;
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
        try
        {
            GetComponentInChildren<PlayerController>().OnMove(context);
        }
        catch { }
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        try
        {
            GetComponentInChildren<PlayerController>().OnLook(context);
        }
        catch { }
    }


    public void OnFire(InputAction.CallbackContext context)
    {
        try
        {
            GetComponentInChildren<PlayerController>().OnFire(context);
        }
        catch { }
    }


    public void OnInsert(InputAction.CallbackContext context)
    {
        try
        {
            GetComponentInChildren<PlayerController>().OnInsert(context);
        }
        catch { }
    }


    public void OnSwitch(InputAction.CallbackContext context)
    {
        try
        {
            GetComponentInChildren<PlayerController>().OnSwitch(context);
        }
        catch { }
    }


    public void OnAim(InputAction.CallbackContext context)
    {
        try
        {
            GetComponentInChildren<PlayerController>().OnAim(context);
        }
        catch { }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        try
        {
            Debug.Log("Show");
            GetComponentInChildren<PlayerController>().OnAction(context);
        }
        catch { }
    }

    public void OnR(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            actualPlayerRealSprite = playersUI[0].sprite;
            atualColor = playersUI[0].cor;
            playersUI.Remove(playersUI[0]);
            playerUI aux;
            aux.cor = atualColor;
            aux.sprite = actualPlayerRealSprite;
            playersUI.Add(aux);
        }
    }

    public void OnL(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            actualPlayerRealSprite = playersUI[0].sprite;
            atualColor = playersUI[0].cor;
            playersUI.Remove(playersUI[0]);
            playerUI aux;
            aux.cor = atualColor;
            aux.sprite = actualPlayerRealSprite;
            playersUI.Add(aux);
        }
    }
    #endregion
}
