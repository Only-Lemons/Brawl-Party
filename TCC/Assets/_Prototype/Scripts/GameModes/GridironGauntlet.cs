using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridironGauntlet : IGameMode
{
    GameObject _Monster = Resources.Load("Mecanicas/Monster") as GameObject;

    public void Action(PlayerController player)
    {
    }

    public void DeathRule(PlayerController player)
    {

    }

    public void FinishGame()
    {

    }

    public void MovementRule(Vector3 dir, Transform player, float speed)
    {

    }

    public void PointRule(PlayerController player)
    {

    }

    public void RotationRule(Vector3 dir, Transform player)
    {

    }

    public void StartGame()
    {

    }

    public void WinRule()
    {

    }
}
