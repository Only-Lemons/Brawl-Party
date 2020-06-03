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

    private AnimationManager AnimManager => _animManager;

    [Header("Animation Refferences")]
    [SerializeField]
    private AnimationManager _animManager = null;

    bool inputBool = false;

    public void Abrir(GameObject obj)
    {
        obj.SetActive(true);
        menuInicial.SetActive(false);
        AudioController.Instance.PlayAudio("Click");

    }

    public void SetSelectedObj(GameObject obj)
    {
        es.SetSelectedGameObject(obj);
    }

    private void Update()
    {
        if (firstScene.activeSelf && Input.anyKey)
        {
            AnimManager.FirstToHome();
            es.SetSelectedGameObject(menuInicialButton);
        }

        else if ((Mathf.Abs(Input.GetAxis("Horizontal")) >= 0.45f || Mathf.Abs(Input.GetAxis("Vertical")) >= 0.45f) && inputBool == false)
        {
            inputBool = true;
            AudioController.Instance.PlayAudio("Select");
        }

        else if (!(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            inputBool = false;
            
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
        AudioController.Instance.PlayAudio("Return");
    }

}
