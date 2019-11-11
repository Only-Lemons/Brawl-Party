using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

[System.Serializable]
public struct playerUI
{
    public Sprite splash,selectedSplash;
    public string charName;
    public Color charColor;
}


public class PlayerSelect : MonoBehaviour, Inputs.IPlayerActions
{
    public List<playerUI> playersUI = new List<playerUI>();
    public Material playerMaterial;
    public PlayerController PlayerGame;


    private int selectedCharIndex;
    [HideInInspector]
    public Color desiredColor;
    [HideInInspector]
    public bool hasEntered = false,
             canNavigate = true;

    [Header("Default Sprites")]
    [SerializeField] private string defaultName;
    [SerializeField] private Sprite defaultSplash;
    [SerializeField] private Color defaultColor;

    [Header("UI References")]
    [SerializeField] public TextMeshProUGUI characterName;
    [SerializeField] public Image characterSplash;
    [SerializeField] public Image backgroundColor;

    private void Start()
    {
        OnEnter();
    }

    public void OnEnter()
    {
        if (!hasEntered)
        {
            characterName.text = playersUI[0].charName;
            characterSplash.sprite = playersUI[0].splash;
            backgroundColor.color = playersUI[0].charColor;
            hasEntered = true;
        }
    }



    public void UpdateCharSelectUi()
    {
        characterSplash.sprite = playersUI[selectedCharIndex].splash;
        characterName.text = playersUI[selectedCharIndex].charName;
        desiredColor = playersUI[selectedCharIndex].charColor;
        backgroundColor.color = desiredColor;
    }




    #region InputSystem Events
    public void OnUP(InputAction.CallbackContext context)
    {
      

    }
    public void OnConfirmed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (hasEntered)
            {
                backgroundColor.color = Color.green;
                playersUI.Remove(playersUI[selectedCharIndex]);
                //Debug.Log("Removed" + charList[selectedCharIndex].charName);
                canNavigate = false;
            }
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
            if (canNavigate)
            {
                if (hasEntered)
                {
                    selectedCharIndex++;
                    if (selectedCharIndex == playersUI.Count)
                        selectedCharIndex = 0;

                    UpdateCharSelectUi();
                }
            }
        }
    }

    public void OnL(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (canNavigate)
            {
                if (hasEntered)
                {
                    selectedCharIndex--;
                    if (selectedCharIndex < 0)
                        selectedCharIndex = playersUI.Count - 1;

                    UpdateCharSelectUi();
                }
            }
        }
    }
    #endregion
}
