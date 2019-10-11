using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();

    public void addPlayerToCanvas()
    {
            GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Filho");
            for (int i = 0; i < jogadores.Length; i++)
            {
               if (!players.Exists(x => x == jogadores[i]))
                    players.Add(jogadores[i]);
            }
            if (players.Count > 0)
            {
                foreach (GameObject player in players)
                {
                    player.transform.SetParent(GameManager.Instance.transform.GetChild(0).GetChild(1));
                    GameManager.Instance.playersPanels.Add(player);
                }
            }
    }

}
