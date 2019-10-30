using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dominion : IGameMode
{
    public void HitRule(PlayerController player)
    {
      
    }

    public void Update()
    {
       
    }

    public void PointRule(PlayerController player)
    {
     
    }

    public void StartGame()
    {

    }

    public void WinRule()
    {

    }

    public void MovementRule(Vector3 dir, Transform player, float speed)
    {
        player.position += dir * speed * Time.deltaTime;
    }

    public void RotationRule(Vector3 dir, Transform player)
    {
        Quaternion _targetRotation = Quaternion.identity;
        if (dir != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(dir);

        }
        player.rotation = Quaternion.Lerp(_targetRotation, Quaternion.identity, Time.deltaTime);
    }

    public void Action(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
