using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Singleton;

    public IGameMode gameMode;
    
    public TerrainController tileManager;
    public PlayerManager playerManager;
    public UIManager uIManager;
    public ArmaManager armaManager;



    public Text time;

    void Awake()
    {
  
        Singleton = this;
        tileManager = GetComponent<TerrainController>();
        playerManager = GetComponent<PlayerManager>();
        uIManager = GetComponent<UIManager>();

   


    }

    private void Start()
    {
        GameManager.Instance.TryGetGameController();
        gameMode.StartGame();

    }
    private void Update()
    {
        gameMode.FinishGame();   
    }

}
