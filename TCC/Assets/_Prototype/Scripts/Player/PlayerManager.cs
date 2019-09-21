using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerController> Players = new List<PlayerController>();
    public Dictionary<PlayerController, float> playerMortos = new Dictionary<PlayerController, float>();
    public List<PlayerController> playerMortosPrefabs = new List<PlayerController>();
    public GameObject prefabPlayer;
    


    // Start is called before the first frame update
    void Awake()
    {
        setPlayerInScene();
       
    }
    void setPlayerInScene()
    {
      PlayerController[] aux =  GameController.FindObjectsOfType<PlayerController>();
        for (int i = 0; i < aux.Length; i++)
            Players.Add(aux[i]);
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
                if(playerMortos[playerMortosPrefabs[i]] <= 0)
                {
                    playerMortosPrefabs[i].gameObject.SetActive(true);
                    playerMortos.Remove(playerMortosPrefabs[i]);
                    playerMortosPrefabs.Remove(playerMortosPrefabs[i]);
                }


            }
        }
    }


}
