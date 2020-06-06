using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VitoriaScene : MonoBehaviour
{
    public GameObject playerWin;
    public GameObject playerWinDefault;
    //public AnimatorController animator;
    public Material playerMaterial;
    int maiorPonto = 0;
    public GameObject painel;
    public Text texto;
    void Start()
    {
	    Time.timeScale = 1;

        GameObject GO = Instantiate(GameManager.Instance.objVencedor,playerWin.transform.position,Quaternion.identity);

        //GO.GetComponent<Animator>().runtimeAnimatorController = animator;

        //playerMaterial.color = GameManager.Instance.corVencedor;

        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            for (int j = 0; j < GameManager.Instance.playersPanels.Count; j++)
            {
                if(j!=i)
                {
                    if (GameManager.Instance.playersPontos[GameManager.Instance.playersPanels[i]] == GameManager.Instance.playersPontos[GameManager.Instance.playersPanels[j]])
                        if (GameManager.Instance.playersPontos[GameManager.Instance.playersPanels[i]] == GameManager.Instance.maiorPonto)
                            if (GameManager.Instance.playersPontos[GameManager.Instance.playersPanels[j]] == GameManager.Instance.maiorPonto)
                                GameManager.Instance.empatou = true;
                }
            }
        }
        

        if (GameManager.Instance.empatou)
        {
            AudioController.Instance.PlayAudio("Win");
            texto.text = "DRAW! MORE THAN ONE PLAYER WON!";
            playerWin.SetActive(false);
        }
        else
        {
            AudioController.Instance.PlayAudio("Win");
            texto.text = "WINNER!";
            playerWin.SetActive(true);
        }

        Destroy(GameManager.Instance.gameObject);

        StartCoroutine(CarregarCena());
    }

    IEnumerator CarregarCena()
    {
        yield return new WaitForSeconds(4f);
        painel.SetActive(true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
