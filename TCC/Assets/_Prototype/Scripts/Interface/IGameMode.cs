using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface  IGameMode 
{
    /// <summary>
    /// 
    /// </summary>
    void StartGame();
    void FinishGame();
    void PointRule(PlayerController player);
    void WinRule();
    void DeathRule(PlayerController player);
 
}
