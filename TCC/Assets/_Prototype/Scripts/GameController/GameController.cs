using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        timeComecar = 4;
        comecou = false;
        comecouContar = false;
     
    }
    private void Start()
    {
        painelPontos.SetActive(false);
        tileManager = GetComponent<TerrainController>();
        playerManager = GetComponent<PlayerManager>();
        uIManager = GetComponent<UIManager>();

        GameManager.Instance.TryGetGameController();
        gameMode.StartGame();
        comecouContar = true;

        timeComecarText.CrossFadeAlpha(0, 5, false);

    }



    public void FinishGame()
    {
        for (int i = 0; i < playerManager.playersControllers.Count; i++)
        {
            playerManager.playersControllers[i].gameObject.SetActive(true);
            pontos[i].text = GameManager.Instance.pontosGeral[i].ToString();
            pontos[i].color = playerManager.playersControllers[i].gameObject.GetComponent<PlayerSelect>().playerMaterial.color;
            personagens[i].sprite = playerManager.playersControllers[i].player.sprite;
    
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
                time.text = "COMEÇOU!";
                timeComecarText.text = "COMEÇOU!";
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
            timeComecarText.text = "ACABOU!";

    }
}