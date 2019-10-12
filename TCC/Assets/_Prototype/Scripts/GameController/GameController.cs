using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController singleton;

    public IGameMode gameMode;
    [HideInInspector]
    public TerrainController tileManager;
    [HideInInspector]
    public PlayerManager playerManager;
    [HideInInspector]
    public UIManager uIManager;
    public Text time;

    void OnEnable()
    {
        singleton = this;
   
    }
    private void Start()
    {
        tileManager = GetComponent<TerrainController>();
        playerManager = GetComponent<PlayerManager>();
        uIManager = GetComponent<UIManager>();

        GameManager.Instance.TryGetGameController();
        gameMode.StartGame();

    }
    private void Update()
    {
      gameMode.FinishGame();   
    }
}
