using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[System.Serializable]
public struct playerUIElements
{
    public Image colorBase;
    public Text points;
    public Slider hp;
    public Text hpText;
    public Slider ammo;
    public Text ammoText;
    public Slider passive;
    public Text passiveText;
    public Image Respawn;
    public Image gun;
    public Image gun2;
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
       // adcionarPlayerControlador();
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
            //player.GetComponentInChildren<Camera>().enabled = false;
            //player.GetComponent<PlayerSelect>().enabled = false;
            player.gameObject.transform.position =_tileManager.bases[playersControllers.IndexOf(player)].position;
            player._base = _tileManager.bases[playersControllers.IndexOf(player)].position;
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
                if (playerMortos[playerMortosPrefabs[i]] <= 0 )
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
    public void RemovePlayerofDeath()
    {
        for (int i = 0; i < playerMortos.Count; i++)
        {
                playerMortos.Remove(playerMortosPrefabs[i]);
                playerMortosPrefabs.Remove(playerMortosPrefabs[i]);
        }
    }


   public void adcionarPlayerControlador()
    {
        for (int i = 0; i < playersControllers.Count; i++)
        {
            if(i < GameManager.Instance.playersPanels.Count)
            {
               playersControllers[i].transform.SetParent(GameManager.Instance.playersPanels[i].transform);
               playersControllers[i].transform.GetChild(1).GetComponentInChildren<Renderer>().material.color = GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().desiredColor;
               playersControllers[i].playerSprite = GameManager.Instance.playersPanels[i].GetComponent<PlayerSelect>().characterSplash.sprite;
            }
            else
            {
                //playersControllers.Remove(playersControllers[i]);
                playersControllers[i].gameObject.SetActive(false);
            }
        }
        List<PlayerController> aux = playersControllers.FindAll(x => !x.gameObject.activeSelf);

        foreach (var auxP in aux)
        {
            playersControllers.Remove(playersControllers.Find(x => x == auxP));
        }

    }
}
