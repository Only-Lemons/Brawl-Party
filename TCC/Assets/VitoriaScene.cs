using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VitoriaScene : MonoBehaviour
{
    public Material playerMaterial;
    int maiorPonto = 0;
    public GameObject painel;
    void Start()
    {
        playerMaterial.color = GameManager.Instance.corVencedor;

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
