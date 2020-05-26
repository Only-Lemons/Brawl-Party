using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using OnlyLemons.BrawlParty.UI;


public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menuInicial;
    [SerializeField]
    private GameObject menuInicialButton;
    [SerializeField]
    private GameObject creditos;
    [SerializeField]
    private GameObject config;
    [SerializeField]
    private GameObject gameConfig;
    [SerializeField]
    private GameObject selectMode;
    [SerializeField]
    private GameObject firstScene;
    [SerializeField]
    private EventSystem es;

    private FirstSceneVfx FirstSceneVfx => _firstSceneVfx;

    [Header("Animation Refferences")]
    [SerializeField]
    private FirstSceneVfx _firstSceneVfx = null;


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
            FirstSceneVfx.OnDisappear();
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
