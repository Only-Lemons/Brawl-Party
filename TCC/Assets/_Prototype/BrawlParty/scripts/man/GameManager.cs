using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerInputPrefab;

    public AudioManager2 audioManager;

    public ParticleManager particleManager;

   

    public GameController gameController;

    public int quantTGames = 4;
    public int TimeInGame = 1;
    public int quantGames = 0;
    public GameModes newGameMode;
    public List<int> lastGameModes = new List<int>();
    public List<GameObject> playersPanels = new List<GameObject>();
    
    public Dictionary<GameObject, int> playersPontos = new Dictionary<GameObject, int>();

    public int[] pontosGeral;
    public Vector3 lastPainel;
    int oldScene = 0;
    int maisPl = 0;
    #region LevelInteract
    public int nextLevel;
    #endregion  

    public Text necessarioMaisJogadores;
    public Color corVencedor;
    public bool empatou;

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

    private void Awake()
    {
        audioManager = GetComponent<AudioManager2>();
        particleManager = GetComponent<ParticleManager>();
    }

    private void FixedUpdate()
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 7: // Menu do Personagem
                foreach (GameObject player in playersPanels)
                {
                    //player.transform.GetChild(0).gameObject.SetActive(false);
                    // player.transform.GetChild(1).gameObject.SetActive(true);
                }

                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

                if (playersPanels.Count > 1 && !playersPanels.Find(x => x.GetComponentInChildren<PlayerSelect>().isConfirmed == true))
                {
                    if (nextLevel != 7 && nextLevel != 4 && nextLevel != 8)
                        SceneManager.LoadScene(4); // provisorio
                    else
                        SceneManager.LoadScene(nextLevel); // provisorio

                    necessarioMaisJogadores.text = "";
                    inicializarDicio();

                }
                else if (playersPanels.Count == 1 && !playersPanels.Find(x => x.GetComponentInChildren<PlayerSelect>().isConfirmed == true))
                    necessarioMaisJogadores.text = "Necessário 2 ou mais jogadores para continuar...";


            break;

            case 4: // Loading
            case 9: // ViCtoryScene
                if (playersPanels.Count > 1 && !playersPanels.Find(x => x.GetComponentInChildren<PlayerSelect>().isConfirmed == true))
                {
                    SceneManager.LoadScene(nextLevel); // provisorio
                }
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
        newScene(newGameMode);

        //foreach (GameObject playerComplete in playersPanels)
        //{
        //    if (!gameController.playerManager.playersControllers.Exists(x => x == playerComplete.GetComponent<PlayerController>()))
        //        gameController.playerManager.playersControllers.Add(playerComplete.GetComponent<PlayerController>());
        //    // gameController.playerManager.Players.Add(playerComplete.transform.GetChild(0).GetComponent<PlayerController>());
        //}


    }

    public void newScene(GameModes game)
    {
        //switch (game)
        //{
        //    //case GameModes.GetItRock:
        //    //    gameController.gameMode = new GetItRock(gameController, 30 * TimeInGame);
        //    //    break;
        //    //case GameModes.JhonBeen:
        //    //    gameController.gameMode = new JhonBeen(gameController, 40 * TimeInGame);
        //    //    break;
        //    //case GameModes.SnackAtack:
        //    //    gameController.gameMode = new SnackAtack(gameController, 30 * TimeInGame);
        //    //    break;
        //    //case GameModes.RunGhost:
        //    //    gameController.gameMode = new RunGhost(gameController, 30 * TimeInGame);
        //    //    break;
        //}
    }


    public void PressStart()
    {
        this.transform.GetChild(1).gameObject.SetActive(!this.transform.GetChild(1).gameObject.activeSelf);
        if (this.transform.GetChild(1).gameObject.activeSelf)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }


    public void VoltarMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }


    public void getPlayersMinigame(List<PlayerController> players)
    {
        for(int i = 0; i < players.Count; i++)
        {
            if (i < playersPanels.Count)
            {
                players[i].transform.SetParent(playersPanels[i].transform);
            }
            else
            {

                players[i].gameObject.SetActive(false);
                //players.Remove(players[i]);
                //Destroy(players[i].gameObject);

            }
        }

        // Nao sei o que isso faz sinceramente 
        List<PlayerController> aux = players.FindAll(x => !x.gameObject.activeSelf);
        foreach (var auxP in aux)
        {
            players.Remove(players.Find(x => x == auxP));
        }

    }
    
    public void WinMinigame()
    {
        quantGames ++;
        if(quantGames > quantTGames)
            SceneManager.LoadScene(10, LoadSceneMode.Single); // Tela vitoria jogo 
        else
            SceneManager.LoadScene(9, LoadSceneMode.Single); // Tela vitoria minigame
    }


    void inicializarDicio()
    {
        foreach ( GameObject playerP in playersPanels)
        {
            playersPontos.Add(playerP,0);    
        }
    }

}