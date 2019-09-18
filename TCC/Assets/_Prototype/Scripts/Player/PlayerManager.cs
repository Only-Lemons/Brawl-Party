using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<SOPlayer> playerSOMortos = new List<SOPlayer>();
    public List<PlayerController> Players = new List<PlayerController>();
    public Dictionary<PlayerController, float> playerMortos = new Dictionary<PlayerController, float>();
    public GameObject prefabPlayer;
    


    // Start is called before the first frame update
    void Start()
    {
        setPlayerInScene();
       
    }
    void setPlayerInScene()
    {
       
    }
   
  
}
