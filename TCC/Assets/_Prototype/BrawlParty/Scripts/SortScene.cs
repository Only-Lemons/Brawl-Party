
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SortScene : MonoBehaviour
{

    public List<GameMode> gameModes;
    public List<GameMode> auxMod;
    public Image prefabScene;
    public Image prefabGM;
    public Image prefabGMante, prefabGMpos;
    public int sortGameM;
    bool _Scenep;

    void Start()
    {

        GameManager.Instance.quantTGames = gameModes.Count;
        foreach (int Index in GameManager.Instance.lastGameModes)
        {
            auxMod.Add(gameModes[Index]);
        }

        if (GameManager.Instance.lastGameModes.Count >= GameManager.Instance.quantGames)
        {   
            SceneManager.LoadScene(9);
        }
        else
        {
            do
            {
                sortGameM = Random.Range(0, gameModes.Count);
            } while (GameManager.Instance.lastGameModes.Contains(sortGameM));
        }
        GameManager.Instance.lastGameModes.Add(sortGameM);



        //QUANDO FAZER COLLAB FAVOR TIRAR A PARTE DA CENA DIRECIONADA

        StartCoroutine(StartScene());
    }




    IEnumerator ShowImageGameMode(List<GameMode> gameMode, Image imagem, int Index)
    {
        yield return new WaitForSeconds(.1f);
        if (Index % gameMode.Count + 1 >= gameMode.Count)
        {
            prefabScene.sprite = gameMode[0].spriteScene;
            imagem.sprite = gameMode[0].sprite;
            prefabGMante.sprite = gameMode[gameModes.Count - 1].sprite;
            prefabGMpos.sprite = gameMode[1].sprite;
        }
        else
        {
            prefabScene.sprite = gameMode[Index % gameMode.Count + 1].spriteScene;
            imagem.sprite = gameMode[Index % gameMode.Count + 1].sprite;
            prefabGMante.sprite = gameMode[Index % gameMode.Count].sprite;
            if (Index % gameMode.Count + 2 < gameMode.Count)
                prefabGMpos.sprite = gameMode[Index % gameMode.Count + 2].sprite;
            else prefabGMpos.sprite = gameMode[0].sprite;
        }
        Index++;
        if (Index >= gameModes.Count * 5 && Index % gameModes.Count == sortGameM)
        {

            StartCoroutine(ChangeScene());

        }
        else
        {
            StartCoroutine(ShowImageGameMode(gameModes, prefabGM, Index));
        }
    }
    IEnumerator ChangeScene()
    {
        GameManager.Instance.nextLevel = gameModes[sortGameM].Scene;
        GameManager.Instance.newGameMode = gameModes[sortGameM].gameMode;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);

    }
    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowImageGameMode(gameModes, prefabGM, 0));
    }

}
