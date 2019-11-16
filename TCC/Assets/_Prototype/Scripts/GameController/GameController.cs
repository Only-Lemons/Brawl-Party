using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct playerPoints
{
    public Text pontos;
    public Image image;
    public Image background;

}


public class GameController : MonoBehaviour
{
    public static GameController singleton;

    public bool comecouContar;
    public bool comecou;
    float timeComecar;
    public Text timeComecarText;

    public IGameMode gameMode;
    [HideInInspector]
    public TerrainController tileManager;
    public PlayerManager playerManager;
    [HideInInspector]
    public UIManager uIManager;
    public Text time;
    public List<Text> pontos;
    public List<Image> personagens;
    public List<playerPoints> players = new List<playerPoints>();
    public GameObject painelPontos;
    public List<Sprite> spritePersonagens = new List<Sprite>();
    public List<Slider> posicoesPersonagens = new List<Slider>();


    void OnEnable()
    {
        singleton = this;
        timeComecar = 4;
        comecou = false;
        comecouContar = false;

    }

    void SetarSprites()
    {
        foreach (PlayerController p in GameController.singleton.playerManager.playersControllers)
        {
            spritePersonagens.Add(p.playerSprite);
        }
    }
    private void Start()
    {
        
        painelPontos.SetActive(false);
        tileManager = GetComponent<TerrainController>();
        playerManager = GetComponent<PlayerManager>();
        uIManager = GetComponent<UIManager>();


        pegarPlayerScene(GameObject.FindGameObjectsWithTag("Player"));

        GameManager.Instance.TryGetGameController();
        playerManager.adcionarPlayerControlador();

        gameMode.StartGame();
        comecouContar = true;

        timeComecarText.CrossFadeAlpha(0, 5, false);
        SetarSprites();
    }



    public void FinishGame()
    {
        for (int i = 0; i < playerManager.playersControllers.Count; i++)
        {
            players[i].image.sprite = playerManager.playersControllers[i].playerSprite;
            players[i].background.color = playerManager.playersControllers[i].playerColor;
            players[i].pontos.text = GameManager.Instance.pontosGeral[i].ToString();
        }
        painelPontos.SetActive(true);

        int maiorPonto = 0;
        int maiorPonto2 = 0;
        int maiorPos = 0;
        for (int i = 0; i < GameManager.Instance.pontosGeral.Length; i++)
        {
            for (int j = 0; j < GameManager.Instance.pontosGeral.Length; j++)
            {
                if (i != j)
                {
                    if (GameManager.Instance.pontosGeral[i] > maiorPonto)
                    {
                        maiorPos = i;

                        maiorPonto = GameManager.Instance.pontosGeral[i];
                        if(GameManager.Instance.pontosGeral[i] == GameManager.Instance.pontosGeral[j])
                            maiorPonto2 = GameManager.Instance.pontosGeral[j];
                    }
                }
            }
        }
        if (maiorPonto != maiorPonto2)
        {
            GameManager.Instance.empatou = false;
            GameManager.Instance.corVencedor = playerManager.playersControllers[maiorPos].playerColor;
        }
        else
        {
            GameManager.Instance.empatou = true;
            GameManager.Instance.corVencedor = Color.black;
        }

        StartCoroutine(ChangeScene());

    }
    IEnumerator ChangeScene()
    {


        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(5);
    }
    private void Update()
    {
        if (comecou)
            gameMode.Update();
        ComecarJogo();
    }

    public void ComecarJogo()
    {
        if (comecouContar)
        {
            timeComecar -= Time.deltaTime;
            time.text = timeComecar.ToString("0");

            timeComecarText.text = timeComecar.ToString("0");


            if (timeComecar <= 0.8f)
            {
                time.text = "BEGIN!";
                timeComecarText.text = "BEGIN!";
                timeComecarText.CrossFadeAlpha(1, 0.1f, false);

                if (timeComecar <= 0)
                {
                    timeComecarText.text = "";
                    comecou = true;
                    timeComecar = 4;
                    comecouContar = false;
                }
            }
        }

        if (time.text == "00:00")
            timeComecarText.text = "FINISH!";

    }

    void pegarPlayerScene(GameObject[] players)
    {
        playerManager.playersControllers.Clear();
        for (int i = 0; i < players.Length; i++)
        {
            PlayerController aux = players[i].GetComponent<PlayerController>();

            if (players[i].TryGetComponent<PlayerController>(out aux))
                playerManager.playersControllers.Add(aux);
        }
    }


}