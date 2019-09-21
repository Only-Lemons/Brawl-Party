using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Singleton;

    public IGameMode gameMode;
    
    public TileManager tileManager;
    public PlayerManager playerManager;
    public UIManager uIManager;
    
  

    void Awake()
    {
        
        Singleton = this;
        tileManager = GetComponent<TileManager>();
        playerManager = GetComponent<PlayerManager>();
        uIManager = GetComponent<UIManager>();
        gameMode = new FreeForAll(this, 60);
       
        
    }
    private void Start()
    {

        gameMode.StartGame();

    }
    private void Update()
    {
        gameMode.FinishGame();   
    }


}
