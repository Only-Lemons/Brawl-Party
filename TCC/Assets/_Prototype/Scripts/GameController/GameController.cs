using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public List<Text> pontos;
    public List<Image> personagens;
    public GameObject painelPontos;
    void OnEnable()
    {
        singleton = this;

    }
    private void Start()
    {
        painelPontos.SetActive(false);
        tileManager = GetComponent<TerrainController>();
        playerManager = GetComponent<PlayerManager>();
        uIManager = GetComponent<UIManager>();

        GameManager.Instance.TryGetGameController();
        gameMode.StartGame();

    }
    public void FinishGame()
    {
        for (int i = 0; i < playerManager.playersControllers.Count; i++)
        {
            pontos[i].text = GameManager.Instance.pontosGeral[i].ToString();
            personagens[i].sprite = playerManager.playersControllers[i].player.sprite;
            playerManager.playersControllers[i].gameObject.SetActive(true);
            playerManager.playersControllers[i].ResetarPlayer();
        }
        painelPontos.SetActive(true);
      
        StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(5);

    }
    private void Update()
    {
        gameMode.Update();
    }
}