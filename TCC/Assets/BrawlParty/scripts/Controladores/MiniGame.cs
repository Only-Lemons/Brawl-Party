using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{ 
    protected Dictionary<GameObject, int> playerPoints = new Dictionary<GameObject, int>();
    // ----- Pause Game Variables ----
    public  bool ispauseGame = false;
   
    public GameObject PainelPause;
    public void PauseGame()
    {
        if (!ispauseGame)
        {
            ispauseGame = true;
            PainelPause.SetActive(true);
        }
        else
        {
            ispauseGame = false;
            PainelPause.SetActive(false);

        }
    }
    public abstract void PointRule(PlayerController player);
    public abstract void WinRule();
    public abstract void Action(PlayerController player);
    public abstract void HitRule(PlayerController player);
    public abstract void MovementRule(PlayerController player);
    public abstract void RotationRule(PlayerController player);
    public abstract void Jump(PlayerController player);
}
