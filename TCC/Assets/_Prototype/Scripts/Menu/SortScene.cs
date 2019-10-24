
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SortScene : MonoBehaviour
{
    public GameMode[] gameModes;
    public Image prefabScene;
    public Image prefabGM;
    public Image prefabGMante, prefabGMpos;
    public int sortGameM;
    bool _Scenep;

    void Start()
    {
        if (GameManager.Instance.lastGameModes.Count == GameManager.Instance.quantGames)
            SceneManager.LoadScene(1);
        sortGameM = Random.Range(0, 4);
        if (!GameManager.Instance.lastGameModes.Contains(sortGameM))
            GameManager.Instance.lastGameModes.Add(sortGameM);
        else
        {
            while (GameManager.Instance.lastGameModes.Contains(sortGameM))
            {
                sortGameM = Random.Range(0, 4);
            }
        }
<<<<<<< HEAD

        sortGameM = 0;

=======
      
>>>>>>> c80b12325a9a72385034ca29d1ce6402734054f8
        StartCoroutine(StartScene());
    }


    IEnumerator ShowImageGameMode(GameMode[] gameMode, Image imagem, int Index)
    {
        yield return new WaitForSeconds(.1f);
        if (Index % gameMode.Length + 1 >= gameMode.Length)
        {
            prefabScene.sprite = gameMode[0].spriteScene;
            imagem.sprite = gameMode[0].sprite;
            prefabGMante.sprite = gameMode[gameModes.Length - 1].sprite;
            prefabGMpos.sprite = gameMode[1].sprite;
        }
        else
        {
            prefabScene.sprite = gameMode[Index % gameMode.Length + 1].spriteScene;
            imagem.sprite = gameMode[Index % gameMode.Length + 1].sprite;
            prefabGMante.sprite = gameMode[Index % gameMode.Length].sprite;
            if (Index % gameMode.Length + 2 < gameMode.Length)
                prefabGMpos.sprite = gameMode[Index % gameMode.Length + 2].sprite;
            else prefabGMpos.sprite = gameMode[0].sprite;
        }
        Index++;
        if (Index >= gameModes.Length * 5 && Index % gameModes.Length == sortGameM)
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
