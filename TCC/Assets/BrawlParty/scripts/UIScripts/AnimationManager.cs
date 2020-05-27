using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnlyLemons.BrawlParty.UI;

public class AnimationManager : MonoBehaviour
{
    private FirstSceneVfx FirstSceneVfx => _firstSceneVfx;
    private CreditsVfx CreditsVfx => _creditsVfx;
    private MainMenuVfx MainMenuVfx => _mainMenuVfx;


    [SerializeField]
    private FirstSceneVfx _firstSceneVfx = null;
    [SerializeField]
    private CreditsVfx _creditsVfx = null;
    [SerializeField]
    private MainMenuVfx _mainMenuVfx = null;

    public void FirstToHome() 
    {
        FirstSceneVfx.OnDisappear();
        MainMenuVfx.OnAppear();
        FirstSceneVfx.gameObject.SetActive(false);
        MainMenuVfx.gameObject.SetActive(true);
    }

    public void MenuToCredits() 
    {
        //Add Menu Disappear
        CreditsVfx.OnAppear();
    }

    public void CreditsToMenu() 
    {
        CreditsVfx.OnDisappear();
        MainMenuVfx.OnAppear();
    }
}
