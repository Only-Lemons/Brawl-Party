using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Slider sliderLoad;

    public VideoClip[] videosGameplay = new VideoClip[4];
    public VideoPlayer videoPlayer;

    public Text comoJogar,
                controles;

    

    public GameModes game;


    void Awake()
    {
        // Debug.Log(GameManager.Instance.nextLevel);
        foreach (GameObject panel in GameManager.Instance.playersPanels)
        {
            panel.GetComponentInChildren<PlayerSelect>().isConfirmed = true;
        }

        updateLoading(GameManager.Instance.nextLevel);
        //StartCoroutine(LoadAsynchronously(GameManager.Instance.nextLevel));
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

    public void updateLoading(int val)
    {
        switch (val)
        {
            case 9:
                Debug.Log(1);
                comoJogar.text = "Punch Escape!";
                controles.text = "Walk and Jump!\nButton A, Left Analog";
                videoPlayer.clip = videosGameplay[0];
                break;

            case 10:
                Debug.Log(2);
                comoJogar.text = "Climb To The Top!";
                controles.text = "Press 'A' Fast!\nButton A, Left Analog";
                videoPlayer.clip = videosGameplay[1];
                break;

            case 12:
                Debug.Log(3);
                comoJogar.text = "Don't Touch The Ghosts!";
                controles.text = "Run From The Ghosts!\nLeft Analog";
                videoPlayer.clip = videosGameplay[2];
                break;

            case 11:
                Debug.Log(4);
                comoJogar.text = "Get The Food!";
                controles.text = "Walk!\nLeft Analog";
                videoPlayer.clip = videosGameplay[3];
                break;

            default:
                Debug.Log("Default");
                comoJogar.text = "Deu Ruim";
                controles.text = "Demais";
                videoPlayer.clip = videosGameplay[4];
                break;

        }
    }
}
