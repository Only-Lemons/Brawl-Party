using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItRock : IGameMode
{
    Dictionary<PlayerController, bool> playerMortos = new Dictionary<PlayerController, bool>();
    public void DeathRule(PlayerController player)
    {
        playerMortos[player] = true;
        if (VerifyPlayerMortos())
        {
            WinRule();
        }
    }

    public void FinishGame()
    {
          
       
    }
    bool VerifyPlayerMortos()
    {
        for (int i = 0; i < playerMortos.Count;i++)
        {
            if (playerMortos[GameController.singleton.playerManager.playersControllers[i]] == false)
                return false;
        }
        return true;
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
        InsertPlayerInDates();
    }
    void InsertPlayerInDates()
    {
        foreach(PlayerController player in GameController.singleton.playerManager.playersControllers)
        {
            playerMortos.Add(player, false);
        }
    }
    public void WinRule()
    {
       
    }
}
