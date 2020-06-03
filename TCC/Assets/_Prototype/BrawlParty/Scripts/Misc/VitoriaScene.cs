using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VitoriaScene : MonoBehaviour
{
    public GameObject playerWin;
    public Material playerMaterial;
    int maiorPonto = 0;
    public GameObject painel;
    public Text texto;
    void Start()
    {
	    Time.timeScale = 1;
        playerMaterial.color = GameManager.Instance.corVencedor;
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
        SceneManager.LoadScene(1);
    }
}
