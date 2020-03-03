using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface  IGameMode 
{
    void StartGame();
    void Update();
    void PointRule(PlayerController player);
    void WinRule();

    void Action(PlayerController player);

    void HitRule(PlayerController player);
    void MovementRule(Vector3 dir, Transform player,float speed);
    void RotationRule(Vector3 dir, Transform player);
}
