using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    private int selectedCharIndex;
    private Color desiredColor;

    private bool hasEntered  = false,
                 canNavigate = true;

    [Header("List of Character")]
    [SerializeField] private List<CharacterSelectObject> charList = new List<CharacterSelectObject>();

    [Header("Default Sprites")]
    [SerializeField] private string defaultName;
    [SerializeField] private Sprite defaultSplash;
    [SerializeField] private Color defaultColor;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private Image characterSplash;
    [SerializeField] private Image backgroundColor;

    public void OnEnter()
    {
        if (!hasEntered)
        {
            characterName.text = charList[0].charName;
            characterSplash.sprite = charList[0].splash;
            backgroundColor.color = charList[0].charColor;
            hasEntered = true;
        }
    }

    public void OnChangeCharUp()
    {
        if (canNavigate)
        {
            if (hasEntered)
            {
                selectedCharIndex++;
                if (selectedCharIndex == charList.Count)
                    selectedCharIndex = 0;

                UpdateCharSelectUi();
            }
        }
    }

    public void OnChangeCharDown()
    {
        if (canNavigate)
        {
            if (hasEntered)
            {
                selectedCharIndex--;
                if (selectedCharIndex < 0)
                    selectedCharIndex = charList.Count - 1;

                UpdateCharSelectUi();
            }
        }
    }

    public void OnCancelCharSelected()
    {
        if (hasEntered)
        {
            characterSplash.sprite = defaultSplash;
            characterName.text = defaultName;
            desiredColor = defaultColor;
            backgroundColor.color = desiredColor;

            hasEntered = false;
            canNavigate = true;
        }
    }

    public void OnConfirmChar()
    {
        if (hasEntered)
        {
            backgroundColor.color = Color.green;
            charList.Remove(charList[selectedCharIndex]);
            //Debug.Log("Removed" + charList[selectedCharIndex].charName);
            canNavigate = false;
        }
    }

    public void UpdateCharSelectUi()
    {
        characterSplash.sprite = charList[selectedCharIndex].splash;
        characterName.text = charList[selectedCharIndex].charName;
        desiredColor = charList[selectedCharIndex].charColor;
        backgroundColor.color = desiredColor;
    }

    [System.Serializable]
    public class CharacterSelectObject
    {
        public Sprite splash,  
                      selectedSplash;
        public string charName;
        public Color  charColor;
    }

}
