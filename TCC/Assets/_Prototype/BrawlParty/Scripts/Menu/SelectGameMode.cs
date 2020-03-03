using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SelectGameMode : MonoBehaviour
{
    public GameMode[] gameMode;
    public Image prefabGM;
    public Image prefabGMante, prefabGMpos;
    public int select;
    
    void Start()
    {
        select = 0; 
        ShowImageGameMode(select);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            BeforeMode();
        if (Input.GetKeyDown(KeyCode.RightArrow))
            NextMode();
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            StartCoroutine(ChangeScene());
    }
    IEnumerator ChangeScene()
    {
        GameManager.Instance.nextLevel = gameMode[select % gameMode.Length + 1].Scene;
        GameManager.Instance.newGameMode = gameMode[select % gameMode.Length + 1].gameMode;
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);

    }
    void ShowImageGameMode(int Index)
    {

        if (Index % gameMode.Length + 1 >= gameMode.Length)
        {
       
            prefabGM.sprite = gameMode[0].sprite;
            prefabGMante.sprite = gameMode[gameMode.Length - 1].sprite;
            prefabGMpos.sprite = gameMode[1].sprite;
        }
        else
        {
           
            prefabGM.sprite = gameMode[Index % gameMode.Length + 1].sprite;
            prefabGMante.sprite = gameMode[Index % gameMode.Length].sprite;
            if (Index % gameMode.Length + 2 < gameMode.Length)
                prefabGMpos.sprite = gameMode[Index % gameMode.Length + 2].sprite;
            else prefabGMpos.sprite = gameMode[0].sprite;
        }     
    }
    public void NextMode()
    {
        select++;
        ShowImageGameMode(select);
    }
    public void BeforeMode()
    {
        select--;
        if (select < 0)
            select = gameMode.Length - 1;
        ShowImageGameMode(select);
    }
}
