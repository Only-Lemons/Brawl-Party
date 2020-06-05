using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct playerUI
{


    public Sprite splash, selectedSplash;
    public string charName;
    public Color charColor;
    public GameObject charObject;

    public playerUI(Sprite splash, Sprite selectedSplash, string charName, Color charColor, GameObject charObject)
    {
        this.splash = splash;
        this.selectedSplash = selectedSplash;
        this.charName = charName;
        this.charColor = charColor;
        this.charObject = charObject;
    }
}


public class PlayerSelect : MonoBehaviour, Inputs.IPlayerActions
{
    public List<playerUI> playersUI = new List<playerUI>();
    public Material playerMaterial;
    public PlayerController PlayerGame;


    public int selectedCharIndex = 0;
    [HideInInspector]
    public Color desiredColor;

    public GameObject selectPlayerObject;

    public bool hasEntered = false,
             isConfirmed = true;


    public Sprite selectSprite;
    public string selectName;
    [Header("Default Sprites")]
    [SerializeField] private string defaultName;
    [SerializeField] private Sprite defaultSplash;
    [SerializeField] private Color defaultColor;

    [Header("UI References")]
    [SerializeField] public Text characterName;
    [SerializeField] public Image characterSplash;
    [SerializeField] public Image backgroundColor;

    private void Start()
    {
        OnEnter();
    }

    private void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7 && isConfirmed)
        {
            NonAvailabed();
            UpdateCharSelectUi();
        }

    }

    public void OnEnter()
    {
        if (!hasEntered)
        {
            characterName.text = playersUI[0].charName;
            characterSplash.sprite = playersUI[0].splash;
            backgroundColor.color = playersUI[0].charColor;
            selectPlayerObject = playersUI[0].charObject;

            //GameManager.Instance.playerEnterDif++;
            hasEntered = true;
        }
    }



    public void UpdateCharSelectUi()
    {
        characterSplash.sprite = playersUI[selectedCharIndex].splash;
        characterName.text = playersUI[selectedCharIndex].charName;
        desiredColor = playersUI[selectedCharIndex].charColor;
        selectPlayerObject = playersUI[selectedCharIndex].charObject;
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
            if (SceneManager.GetActiveScene().buildIndex == 7)
            {
                if (isConfirmed)
                {
                    AudioController.Instance.PlayAudio("ConfirmPlayer");
                    backgroundColor.color = Color.green;
                    selectSprite = characterSplash.sprite;
                    selectName = characterName.text;
                    //Debug.Log("Removed" + charList[selectedCharIndex].charName);
                    isConfirmed = false;

                    GameManager.Instance.playerNonAvail.Add(selectedCharIndex);
                }
                else
                {
                    selectSprite = null;
                    backgroundColor.color = playersUI[selectedCharIndex].charColor;
                    isConfirmed = true;

                    GameManager.Instance.playerNonAvail.Remove(selectedCharIndex);
                }

            }
            else if (SceneManager.GetActiveScene().buildIndex == 4 || (SceneManager.GetSceneByBuildIndex(9).isLoaded))
            {
                if (isConfirmed)
                {
                    isConfirmed = false;
                }
                else
                {
                    AudioController.Instance.PlayAudio("ConfirmPlayer");
                    isConfirmed = true;
                }


            }
            else
            {
                GameManager.Instance.PressStart();

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
        if (context.started && isConfirmed && hasEntered)
        {
            if (selectedCharIndex == playersUI.Count)
                selectedCharIndex = 0;
            do
            {
                selectedCharIndex++;
                if (selectedCharIndex >= playersUI.Count)
                    selectedCharIndex = 0;
            }
            while (GameManager.Instance.playerNonAvail.Contains(selectedCharIndex));
            UpdateCharSelectUi();
            AudioController.Instance.PlayAudio("PassChar");
        }
    }

    public void OnL(InputAction.CallbackContext context)
    {
        if (context.started && isConfirmed && hasEntered)
        {
            if (selectedCharIndex == -1)
                selectedCharIndex = playersUI.Count - 1;
            do
            {
                selectedCharIndex--;
                if (selectedCharIndex <= -1)
                    selectedCharIndex = playersUI.Count - 1;
            }
            while (GameManager.Instance.playerNonAvail.Contains(selectedCharIndex));
            UpdateCharSelectUi();
            AudioController.Instance.PlayAudio("PassChar");
        }
    }

    void NonAvailabed()
    {
        if (GameManager.Instance.playerNonAvail.Count > 0)
        {
            if (GameManager.Instance.playerNonAvail.Contains(selectedCharIndex))
            {
                selectedCharIndex--;
                if (selectedCharIndex <= -1)
                    selectedCharIndex = playersUI.Count - 1;
            }
        }
    }
    #endregion
}
