using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSelect : MonoBehaviour, Inputs.ISelectUIActions
{
    public List<SOPlayer> players= new List<SOPlayer>();
    public SOPlayer selectPlayer;
    public Image playerImage;
    public Image playerBackround;
    public bool isConfirmed = false;
    public Transform pai;

    private void Awake()
    {
        playerBackround = GetComponent<Image>();
        playerImage = transform.GetChild(0).GetComponent<Image>(); // levando em conta que tem so player
        pai = GameObject.FindGameObjectsWithTag("Pai");
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
            playerBackround.color = Color.green;
        else
            playerBackround.color = Color.red;

    }


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

    public void OnAdd(InputAction.CallbackContext context)
    {
       this.transform.SetParent()
    }
}
