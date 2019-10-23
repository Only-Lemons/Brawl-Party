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
    public List<int> lastGameModes = new List<int>();
    public List<GameObject> playersPanels = new List<GameObject>();
    public int[] pontosGeral;
    public Vector3 lastPainel;
    int oldScene = 0;
    #region LevelInteract
    public int nextLevel;
    #endregion  
    private void OnEnable()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void FixedUpdate()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 4: // Menu do Personagem
                foreach (GameObject player in playersPanels)
                {
                    //player.transform.GetChild(0).gameObject.SetActive(false);
                    // player.transform.GetChild(1).gameObject.SetActive(true);
                }

                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                if (playersPanels.Count > 0 && !playersPanels.Find(x => x.GetComponentInChildren<PlayerSelect>().isConfirmed == false))
                {
                    SceneManager.LoadScene(5); // provisorio
                }


                break;

            case 7:

                break;

            default:
                if (oldScene != SceneManager.GetActiveScene().buildIndex)
                {
                    foreach (GameObject player in playersPanels)
                    {

                    }
                    // transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    oldScene = SceneManager.GetActiveScene().buildIndex;
                }
                break;
        }
    }

    public void TryGetGameController()
    {

        gameController = GameObject.FindObjectOfType<GameController>();
        foreach (GameObject playerComplete in playersPanels)
        {
            if (!gameController.playerManager.playersControllers.Exists(x => x == playerComplete.GetComponent<PlayerController>()))
                gameController.playerManager.playersControllers.Add(playerComplete.GetComponent<PlayerController>());
            // gameController.playerManager.Players.Add(playerComplete.transform.GetChild(0).GetComponent<PlayerController>());
        }
        newScene(newGameMode);
    }
    public void newScene(GameModes game)
    {
        switch (game)
        {
            case GameModes.CaptureTheFlag:
                gameController.gameMode = new CaptureTheFlag(gameController, 120);
                break;
            case GameModes.FreeForAll:
                gameController.gameMode = new FreeForAll(gameController, 50);
                break;
            case GameModes.GetItRock:
                gameController.gameMode = new GetItRock(gameController, 45);
                break;
            case GameModes.JhonBeen:
                gameController.gameMode = new JhonBeen(gameController, 60);
                break;
        }
    }

}