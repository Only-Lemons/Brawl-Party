using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSelect : MonoBehaviour, Inputs.IPlayerActions
{
    public List<SOPlayer> players= new List<SOPlayer>();
    public SOPlayer selectPlayer;
    public Image playerImage;
    public Image playerBackround;
    public bool isConfirmed = false;
    public PlayerController PlayerGame;

    private void Awake()
    {
        playerBackround = transform.GetChild(0).GetComponent<Image>();
        playerImage = transform.GetChild(0).GetChild(0).GetComponent<Image>(); // levando em conta que tem so player
    }
    private void Start()
    {
        selectPlayer = players[0];
        players.Remove(selectPlayer);
        playerImage.sprite = selectPlayer.sprite;
        players.Add(selectPlayer);
    }
    void Update()
    {
        if (isConfirmed)
        {
            playerBackround.color = Color.green;
            PlayerGame.player = selectPlayer;
        }
        else
            playerBackround.color = Color.red;
    }

    #region InputSystem Events
    public void OnUP(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            selectPlayer = players[0];
            players.Remove(selectPlayer);
            playerImage.sprite = selectPlayer.sprite;
            players.Add(selectPlayer);
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
    #endregion
}
