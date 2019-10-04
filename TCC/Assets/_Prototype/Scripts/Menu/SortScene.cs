
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

    
    IEnumerator ShowImageScene(Scenes[] sprite,Image image, int Index)
    {
        yield return new WaitForSeconds(.1f);
        if (Index % sprite.Length + 1 >= sprite.Length)
            image.sprite = sprite[0].Scene;
        else image.sprite = sprite[Index % sprite.Length + 1].Scene;
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
              StartCoroutine(ShowImageScene(cenas, prefabScene, Index));
        }
    }
    IEnumerator ShowImageGameMode(GameMode[] gameMode, Image imagem, int Index)
    {
        yield return new WaitForSeconds(.1f);
        if(Index % gameMode.Length +1 >= gameMode.Length)
            imagem.sprite = gameMode[0].sprite;
        else imagem.sprite = gameMode[Index % gameMode.Length + 1].sprite;
        Index++;
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
            StartCoroutine(ShowImageGameMode(gameModes, prefabGM, Index));
        }
    }
    IEnumerator ChangeScene()
    {
      
        GameManager.Instance.nextLevel = cenas[sortScene].SceneIndex;
        GameManager.Instance.newGameMode = gameModes[sortGameM].gameMode;      
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
     
    }
   
    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowImageScene(cenas, prefabScene,0));
        StartCoroutine(ShowImageGameMode(gameModes,prefabGM,0));
    }

}
