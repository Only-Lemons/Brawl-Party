using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public List<Player> playerSO = new List<Player>();
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
