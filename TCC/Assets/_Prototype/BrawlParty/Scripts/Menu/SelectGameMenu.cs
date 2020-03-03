using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectGameMenu : MonoBehaviour
{

    public GameMode modoJogo;


    public void mudar()
    {
        GameManager.Instance.nextLevel = modoJogo.Scene;
        GameManager.Instance.newGameMode = modoJogo.gameMode;
        //GameManager.Instance.lastGameModes.Add(GameManager.Instance.nextLevel);
        SceneManager.LoadScene(14);
    }

     IEnumerator ChangeScene()
    {
        GameManager.Instance.nextLevel = modoJogo.Scene;
        GameManager.Instance.newGameMode = modoJogo.gameMode;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);

    }
}
