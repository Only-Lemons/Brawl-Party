using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject playerInputPrefab;
    public IGameMode gameMode;
    public GameController gameController;
    public float TimeInGame = 120;
    public GameModes newGameMode;
    #region LevelInteract
    public int nextLevel;
    #endregion  
    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }      
    }
    public void TryGetGameController()
    {
        if(GameObject.FindObjectOfType<GameController>() != null)
        {
            gameController = GameObject.FindObjectOfType<GameController>();
            newScene(newGameMode);
        }

    }
    public void newScene(GameModes game)
    {
        if (game == GameModes.CaptureTheFlag)
        {
            gameController.gameMode =  new CaptureTheFlag(gameController, TimeInGame);
        }
        else if (game == GameModes.FreeForAll)
        {
            gameController.gameMode = new FreeForAll(gameController, TimeInGame);
        }
    }

    private void Update()
    {

    }


}
