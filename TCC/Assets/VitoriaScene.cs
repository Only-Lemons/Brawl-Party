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
        playerMaterial.color = GameManager.Instance.corVencedor;
        if (GameManager.Instance.empatou)
        {
            texto.text = "DRAW! MORE THAN ONE PLAYER WON!";
            playerWin.SetActive(false);
        }
        else
        {
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
