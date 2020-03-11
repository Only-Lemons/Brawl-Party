using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{ 
    protected Dictionary<PlayerController, int> playerPoints = new Dictionary<PlayerController, int>();

    public abstract void PointRule(PlayerController player);
    public abstract void WinRule();
    public abstract void Action(PlayerController player);
    public abstract void HitRule(PlayerController player);
    public abstract void MovementRule(PlayerController player);
    public abstract void RotationRule(PlayerController player);
    public abstract void Jump(PlayerController player);
}
