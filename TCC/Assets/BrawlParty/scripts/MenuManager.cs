using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject menuInicial;
    [SerializeField]
    GameObject menuInicialButton;
    [SerializeField]
    GameObject creditos;
    [SerializeField]
    GameObject config;
    [SerializeField]
    GameObject gameConfig;
    [SerializeField]
    GameObject selectMode;
    [SerializeField]
    GameObject firstScene;
    [SerializeField]
    EventSystem es;

    public void Abrir(GameObject obj)
    {
        obj.SetActive(true);
        menuInicial.SetActive(false);

    }

    public void SetSelectedObj(GameObject obj)
    {
        es.SetSelectedGameObject(obj);
    }

    private void Update()
    {
        if(firstScene.activeSelf && Input.anyKey)
        {
            firstScene.SetActive(false);
            es.SetSelectedGameObject(menuInicialButton);
            menuInicial.SetActive(true);
        }
    }

    public void OnReturn()
    {
        menuInicial.SetActive(true);
        es.SetSelectedGameObject(menuInicialButton);
        creditos.SetActive(false);
        config.SetActive(false);
        firstScene.SetActive(false);
        gameConfig.SetActive(false);
        selectMode.SetActive(false);
    }

}
