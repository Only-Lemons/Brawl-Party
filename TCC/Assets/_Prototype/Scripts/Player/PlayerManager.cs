using System.Collections;
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
    public List<PlayerController> playersControllers = new List<PlayerController>();
    public Dictionary<PlayerController, float> playerMortos = new Dictionary<PlayerController, float>();
    public List<PlayerController> playerMortosPrefabs = new List<PlayerController>();
    public List<playerUIElements> playersUI = new List<playerUIElements>();
    public float timeRespawn;
    TerrainController _tileManager;


    void Awake()
    {
        setPlayerInScene(); 
    }
    private void Start()
    {
        _tileManager = GameController.singleton.tileManager;
        MovePlayerBase();
    }
    private void Update()
    {
        DeathPlayerVerifity();
    }

    void MovePlayerBase()
    {
        foreach (PlayerController player in playersControllers)
        {
            player.gameObject.transform.position =_tileManager.bases[playersControllers.IndexOf(player)];
            player._base = _tileManager.bases[playersControllers.IndexOf(player)];
        }
    }
    void setPlayerInScene()
    {
        PlayerController[] aux = GameObject.FindObjectsOfType<PlayerController>();
        for (int i = 0; i < aux.Length; i++)
        {
            //playersControllers.Add(aux[i]);

            aux[i].playerUI = playersUI[i];
        }
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
