﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[System.Serializable]
public struct playerUIElements
{
    public Text points;
    public Slider hp;
    public Text hpText;
    public Slider ammo;
    public Text ammoText;
    public Slider passive;
    public Text passiveText;
    public Image Respawn;

    public Image gun;
    public Image character;
    
    //adcionar aqui outros elementos da ui
}

public class PlayerManager : MonoBehaviour
{
    public List<PlayerController> Players = new List<PlayerController>();
    public Dictionary<PlayerController, float> playerMortos = new Dictionary<PlayerController, float>();
    public List<PlayerController> playerMortosPrefabs = new List<PlayerController>();
    public GameObject prefabPlayer;
    public List<playerUIElements> playersUI = new List<playerUIElements>();
    public float timeRespawn;



    void Awake()
    {
        setPlayerInScene();
     
    }
    private void Start()
    {
        MovePlayerBase();
    }
    void MovePlayerBase()
    {
        foreach (PlayerController player in Players)
        {
            player.gameObject.transform.position = GameController.Singleton.tileManager.bases[Players.IndexOf(player)];
            player._base = GameController.Singleton.tileManager.bases[Players.IndexOf(player)];
        }
    }
    void setPlayerInScene()
    {
        PlayerController[] aux = GameObject.FindObjectsOfType<PlayerController>();
        foreach (PlayerController player  in aux)
        {
            Players.Add(player);
            playersUI.Add(player.playerUI);
        }    
    }

    private void Update()
    {
        DeathPlayerVerifity();
    }

    void DeathPlayerVerifity()
    {
        if(playerMortos.Count > 0)
        {
            for (int i = 0; i < playerMortos.Count; i++)
            {
                playerMortos[playerMortosPrefabs[i]] -= Time.deltaTime;
                playerMortosPrefabs[i].playerUI.Respawn.fillAmount =  playerMortos[playerMortosPrefabs[i]] / timeRespawn;
                if (playerMortos[playerMortosPrefabs[i]] <= 0)
                {
                    playerMortosPrefabs[i].playerUI.Respawn.enabled = false;
                    playerMortosPrefabs[i].ResetarPlayer();
                    playerMortosPrefabs[i].gameObject.SetActive(true);
                    playerMortos.Remove(playerMortosPrefabs[i]);
                    playerMortosPrefabs.Remove(playerMortosPrefabs[i]);
                }


            }
        }
    }


}
