using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSelectManager : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public GameObject renderPlayer;
    public List<RenderTexture> renderTextures = new List<RenderTexture>();
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
                //jogadores[i].GetComponentInChildren<Camera>().targetTexture = renderTextures[i];
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
                GameManager.Instance.playersPanels.Add(player);
            }
        }
    }



}
