using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MiniGame : MonoBehaviour
{ 
    protected Dictionary<GameObject, int> playerPoints = new Dictionary<GameObject, int>();

    public abstract void PointRule(PlayerController player);
    public abstract void WinRule();
    public abstract void Action(PlayerController player);
    public abstract void HitRule(PlayerController player);
    public abstract void MovementRule(PlayerController player);
    public abstract void RotationRule(PlayerController player);
    public abstract void Jump(PlayerController player);

    public void InstanciarPlayer(Transform pos, GameObject prefab)
    {
        Instantiate(prefab, pos.position, Quaternion.identity, pos);
    }
}
