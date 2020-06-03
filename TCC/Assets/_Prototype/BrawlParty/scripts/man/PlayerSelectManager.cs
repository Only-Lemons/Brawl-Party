using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerSelectManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public GameObject[] renderPlayer;
    public List<Image> playerSprite = new List<Image>();
    public List<Vector3> pos = new List<Vector3>();

    public void addPlayerToCanvas()
    {
        GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < jogadores.Length; i++)
        {
            if (!players.Exists(x => x == jogadores[i]))
            {
                players.Add(jogadores[i]);
                AudioController.Instance.PlayAudio("EnterPlayer");

                // jogadores[i].transform.position = pos[i];
            }


        }

        if (players.Count > 0)
        {
            for (int i = 0; i < players.Count; i++)
            {
                players[i].transform.SetParent(GameManager.Instance.transform);

                players[i].GetComponent<PlayerSelect>().characterSplash = renderPlayer[i].transform.GetChild(1).GetChild(0).GetComponent<Image>();
                players[i].GetComponent<PlayerSelect>().backgroundColor = renderPlayer[i].transform.GetChild(1).GetComponent<Image>();
                players[i].GetComponent<PlayerSelect>().characterName = renderPlayer[i].transform.GetChild(2).GetComponent<Text>();

                if (!GameManager.Instance.playersPanels.Exists(x => x == players[i]))
                {
                    GameManager.Instance.playersPanels.Add(players[i]);

                }
            }
        }
    }
}




