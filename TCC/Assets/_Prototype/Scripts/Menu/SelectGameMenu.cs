using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectGameMenu : MonoBehaviour
{

    public GameMode modoJogo;


    public void mudar()
    {
        StartCoroutine(ChangeScene());
    }

     IEnumerator ChangeScene()
    {
        GameManager.Instance.nextLevel = modoJogo.Scene;
        GameManager.Instance.newGameMode = modoJogo.gameMode;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(4);

    }
}
