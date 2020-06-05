using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class LoadScene : MonoBehaviour
{
    public Slider sliderLoad;

    public VideoClip[] videosGameplay = new VideoClip[4];
    public Sprite[] spritesMinigames = new Sprite[4];
    public VideoPlayer videoPlayer;

    public Text minigameName,
                minigameDescription;

    public Image spriteMinigameP,
                 spriteMinigameG;

    public GameModes game;

    public Image[] spritePlayer;
    public Text[] confirmPlayer;


    void Awake()
    {
        // Debug.Log(GameManager.Instance.nextLevel);
        foreach (GameObject panel in GameManager.Instance.playersPanels)
        {
            panel.GetComponentInChildren<PlayerSelect>().isConfirmed = true;
        }

        updateLoading(GameManager.Instance.nextLevel);

        AtualizarSprites();
        //StartCoroutine(LoadAsynchronously(GameManager.Instance.nextLevel));
    }

    private void Start()
    {
        AudioController.Instance.PlayAudio("Loading");
    }
    IEnumerator LoadAsynchronously(int scene)
    {
        yield return new WaitForSeconds(3);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        while (!operation.isDone)
        {
            sliderLoad.value = operation.progress / 9;
            yield return null;
        }
    }

    private void Update()
    {
        ConfirmarTutorial();
    }

    public void updateLoading(int val)
    {
        switch (val)
        {
            case 11:
                minigameName.text = "Maze";
                minigameDescription.text = "Escape os the gloves, walk with the left analog and jump with 'A'";
                videoPlayer.clip = videosGameplay[3];
                spriteMinigameP.sprite = spritesMinigames[0];
                spriteMinigameG.sprite = spritesMinigames[0];
                break;
            case 12:
                minigameName.text = "Punch Escape";
                minigameDescription.text = "Escape os the gloves, walk with the left analog and jump with 'A'";
                videoPlayer.clip = videosGameplay[0];
                spriteMinigameP.sprite = spritesMinigames[0];
                spriteMinigameG.sprite = spritesMinigames[0];
                break;

            case 13:
                minigameName.text = "Jhon Bean";
                minigameDescription.text = "Climb fast to the top, press 'A' to go faster, and avoid of the obstacles with the Left Analog";
                videoPlayer.clip = videosGameplay[1];
                spriteMinigameP.sprite = spritesMinigames[1];
                spriteMinigameG.sprite = spritesMinigames[1];
                break;
            case 14:
                minigameName.text = "Ghost Run";
                minigameDescription.text = "Run From The Ghosts! stay alive to win the game";
                videoPlayer.clip = videosGameplay[2];
                spriteMinigameP.sprite = spritesMinigames[2];
                spriteMinigameG.sprite = spritesMinigames[2];
                break;

            case 15:
                minigameName.text = "Fall Birds";
                minigameDescription.text = "Run From The Ghosts! stay alive to win the game";
                videoPlayer.clip = videosGameplay[2];
                spriteMinigameP.sprite = spritesMinigames[2];
                spriteMinigameG.sprite = spritesMinigames[2];
                break;


            default:
                Debug.Log("Default");
                minigameName.text = "Deu Ruim";
                minigameDescription.text = "Demais";
                videoPlayer.clip = videosGameplay[4];
                break;

        }
    }

    void AtualizarSprites()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            spritePlayer[i].sprite = GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().selectSprite;
        }
         
    }
    void ConfirmarTutorial()
    {
        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if (!GameManager.Instance.playersPanels[i].GetComponentInChildren<PlayerSelect>().isConfirmed)
                confirmPlayer[i].text = "OK!";
            else
                confirmPlayer[i].text = "Waiting...";
        }
    }
}
