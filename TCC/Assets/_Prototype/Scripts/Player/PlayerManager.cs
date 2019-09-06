using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<SOPlayer> playerSO = new List<SOPlayer>();
    public List<PlayerController> Players = new List<PlayerController>();
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
