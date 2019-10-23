using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSelectManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public GameObject renderPlayer;


    public void addPlayerToCanvas()
    {
            GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Player");
            for (int i = 0; i < jogadores.Length; i++)
            {
               players.Add(jogadores[i]); 
            }
            if (players.Count > 0)
            {
                foreach (GameObject player in players)
                {
                    GameObject acualRenderPlayer = Instantiate(renderPlayer, this.transform);
                    acualRenderPlayer.GetComponent<RawImage>().texture = player.GetComponentInChildren<Camera>().targetTexture;


                    player.transform.SetParent(GameManager.Instance.transform);
                    GameManager.Instance.playersPanels.Add(player);
                }
            }
    }



}
