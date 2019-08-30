using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public List<Tile> Tiles = new List<Tile>();
   
    GameController gameController;
    private void Start()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }
    void FixedUpdate()
    {
        for (int i = 0; i < gameController.getPersonagens().Count; i++)
        {
            RaycastHit raycast;
         
                if (Physics.Raycast(gameController.getPersonagens()[i].transform.position, new Vector3(0, -1, 0),out raycast))
                {
                    if(raycast.collider.gameObject.GetComponent<IInteractable>() != null)
                        raycast.collider.gameObject.GetComponent<IInteractable>().Interagir(gameController.getPersonagens()[i]);
                    Debug.Log(raycast.collider.gameObject.GetComponent<Tile>().Peso);
                 }
                else
                {
                    Debug.Log("Não colidiu");
                  
                }
            
            
        }
    }
}
