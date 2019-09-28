
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SortScene : MonoBehaviour
{
    public Scenes[] cenas;
    public GameMode[] gameModes;
    public Image prefabScene;
    public Image prefabGM;
    public int sortScene;
    public int sortGameM;
    bool Scenep, Gamep;

    void Start()
    {
     
       sortScene = Random.Range(0, cenas.Length);
       sortGameM = Random.Range(0, gameModes.Length);
       StartCoroutine(StartScene());
    }

    
    IEnumerator ShowImageScene(Sprite sprite,Image image, int Index)
    {
        yield return new WaitForSeconds(.1f);
        image.sprite = sprite;
        Index++;
        Debug.Log(Index % cenas.Length);
        if (Index >= cenas.Length * 5 && Index%cenas.Length == sortScene)
        {
            if (Gamep)
            {
                StartCoroutine(ChangeScene());
            }
            Scenep = true;
        }
        else {
              StartCoroutine(ShowImageScene(cenas[Index % cenas.Length].Scene, prefabScene, Index));
        }
    }
    IEnumerator ShowImageGameMode(Sprite sprite, Image imagem, int Index)
    {
        yield return new WaitForSeconds(.1f);
        imagem.sprite = sprite;
        Index++;
        Debug.Log(Index % gameModes.Length);
        if (Index >= gameModes.Length * 5 && Index%gameModes.Length == sortGameM)
        {
            if (Scenep)
            {
                StartCoroutine(ChangeScene());
            }
            Gamep = true;
        }
        else
        {
            StartCoroutine(ShowImageGameMode(gameModes[Index % gameModes.Length].sprite, prefabGM, Index));
        }
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.nextLevel = cenas[sortScene].SceneIndex;
        GameManager.Instance.newGameMode = gameModes[sortGameM].gameMode;
        SceneManager.LoadScene(3);
     
    }
   
    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowImageScene(cenas[0].Scene, prefabScene,0));
        StartCoroutine(ShowImageGameMode(gameModes[0].sprite,prefabGM,0));
    }

}
