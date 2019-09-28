using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Singleton;

    public IGameMode gameMode;
    
    public TileManager tileManager;
    public PlayerManager playerManager;
    public UIManager uIManager;

    public Text time;

    void Awake()
    {
  
        Singleton = this;
        tileManager = GetComponent<TileManager>();
        playerManager = GetComponent<PlayerManager>();
        uIManager = GetComponent<UIManager>();
        GameManager.Instance.TryGetGameController();


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
