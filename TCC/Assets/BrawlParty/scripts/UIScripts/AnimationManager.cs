using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OnlyLemons.BrawlParty.UI;

public class AnimationManager : MonoBehaviour
{
    private FirstSceneVfx FirstSceneVfx => _firstSceneVfx;
    private CreditsVfx CreditsVfx => _creditsVfx;
    private MainMenuVfx MainMenuVfx => _mainMenuVfx;
    private MenuManager MenuManager => _menuManager;
    private ExitPopupVfx ExitVfx => _exitVfx;

    [SerializeField]
    private FirstSceneVfx _firstSceneVfx = null;
    [SerializeField]
    private CreditsVfx _creditsVfx = null;
    [SerializeField]
    private MainMenuVfx _mainMenuVfx = null;
    [SerializeField]
    private ExitPopupVfx _exitVfx = null;
    [SerializeField]
    private MenuManager _menuManager = null;

    public void FirstToHome() 
    {
        FirstSceneVfx.OnDisappear();
        MainMenuVfx.OnAppear();
        FirstSceneVfx.gameObject.SetActive(false);
        MainMenuVfx.gameObject.SetActive(true);

        MenuManager.ActiveScene = "Menu";
    }

    public void MenuToCredits() 
    {
        CreditsVfx.OnAppear();
        MainMenuVfx.Disappear();
        MenuManager.ActiveScene = "Credits";

    }

    public void CreditsToMenu() 
    {
        CreditsVfx.OnDisappear();
        MainMenuVfx.OnAppear();
        MenuManager.ActiveScene = "Menu";
    }

    public void MenuToSettings() 
    {
        MenuManager.ActiveScene = "Settings";
    }

    public void SettingsToMenu()
    {
        MainMenuVfx.OnAppear();
        MenuManager.ActiveScene = "Menu";
    }

    public void MenuToSingle() 
    {
        MenuManager.ActiveScene = "Single";
    }

    public void SingleToMenu()
    {
        MenuManager.ActiveScene = "Menu";
    }

    public void ExitGame() 
    {
        ExitVfx.OnAppear();
        MenuManager.ActiveScene = "Exit";
    }

    public void DontExit() 
    {
        ExitVfx.OnDisappear();
        MenuManager.ActiveScene = "Menu";
    }
}
