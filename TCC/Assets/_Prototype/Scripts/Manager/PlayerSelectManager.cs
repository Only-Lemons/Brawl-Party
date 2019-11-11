using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerSelectManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public GameObject renderPlayer;
    public List<Image> playerSprite = new List<Image>();
    public List<Vector3> pos = new List<Vector3>();

    public void addPlayerToCanvas()
    {
        GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }


        for (int i = 0; i < jogadores.Length; i++)
        {
            if (!players.Exists(x => x == jogadores[i]))
            {
                players.Add(jogadores[i]);

                // jogadores[i].transform.position = pos[i];
            }


        }

        if (players.Count > 0)
        {
            foreach (GameObject player in players)
            {

                GameObject acualRenderPlayer = Instantiate(renderPlayer, this.transform);
                //acualRenderPlayer.GetComponent<RawImage>().texture = player.GetComponentInChildren<Camera>().targetTexture;


                player.transform.SetParent(GameManager.Instance.transform);
                player.GetComponent<PlayerSelect>().characterSplash = acualRenderPlayer.transform.GetChild(1).GetComponent<Image>();
                player.GetComponent<PlayerSelect>().backgroundColor = acualRenderPlayer.transform.GetChild(0).GetComponent<Image>();
                player.GetComponent<PlayerSelect>().characterName = acualRenderPlayer.GetComponentInChildren<TextMeshProUGUI>();
                if (!GameManager.Instance.playersPanels.Exists(x => x  == player))
                {
                    GameManager.Instance.playersPanels.Add(player);
                 
                }
                    
            }
        }
    }



}
