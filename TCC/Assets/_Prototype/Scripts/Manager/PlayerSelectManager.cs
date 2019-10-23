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
    public List<LayerMask> layers = new List<LayerMask>();

    public void addPlayerToCanvas()
    {
            GameObject[] jogadores = GameObject.FindGameObjectsWithTag("Player");
            if(transform.childCount > 0 )
            {
                for (int i = 0; i <= this.transform.childCount; i++)
                {
                    Destroy(this.transform.GetChild(0).gameObject);
                }
            }
            
            for (int i = 0; i < jogadores.Length; i++)
            {
                if(!players.Exists(x => x == jogadores[i]))
                {
                    players.Add(jogadores[i]);
                    jogadores[i].GetComponentInChildren<Camera>().targetTexture = renderTextures[i];
                    jogadores[i].transform.GetChild(1).GetChild(0).gameObject.layer = LayerMask.NameToLayer(i.ToString());
                    Debug.Log(LayerMask.NameToLayer(i.ToString()));
                    //jogadores[i].GetComponentInChildren<Camera>().cullingMask = jogadores[i].transform.GetChild(1).GetChild(0).gameObject.layer;
                }
                
                
            }

            if (players.Count > 0)
            {
                foreach (GameObject player in players)
                {
                    
                    GameObject acualRenderPlayer = Instantiate(renderPlayer, this.transform);
                    acualRenderPlayer.GetComponent<RawImage>().texture = new RenderTexture(256, 256, 256);   
                    acualRenderPlayer.GetComponent<RawImage>().texture = player.GetComponentInChildren<Camera>().targetTexture;


                    player.transform.SetParent(GameManager.Instance.transform);
                    GameManager.Instance.playersPanels.Add(player);
                }
            }
    }



}
