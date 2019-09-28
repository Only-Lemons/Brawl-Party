using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SortSceneBoss : MonoBehaviour
{
    public Scenes[] cenas;
    public Image prefabScene;
    public int sortScene;
    void Start()
    {
        sortScene = Random.Range(0, cenas.Length);
        StartCoroutine(StartScene());
    }

    IEnumerator ShowImageScene(Sprite sprite, Image image, int Index)
    {
        yield return new WaitForSeconds(.1f);
        image.sprite = sprite;
        Index++;
        Debug.Log(Index % cenas.Length);
        if (Index >= cenas.Length * 5 && Index % cenas.Length == sortScene)
        {
                StartCoroutine(ChangeScene());
        }
        else
        {
            StartCoroutine(ShowImageScene(cenas[Index % cenas.Length].Scene, prefabScene, Index));
        }
    }
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.nextLevel = cenas[sortScene].SceneIndex;
        GameManager.Instance.newGameMode = GameModes.Boss;
        SceneManager.LoadScene(3);

    }
    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowImageScene(cenas[0].Scene, prefabScene, 0));

    }
}
