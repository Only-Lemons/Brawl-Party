using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public static TerrainController instance;


    public TileController[] tilesInstanciados;
   
 
    
    GameController gameController;

    void Awake()
    {
        instance = this;
    }



    private void Start()
    {    
        gameController = GameObject.FindObjectOfType<GameController>();
    }


    //void FixedUpdate()
    //{
        //for (int i = 0; i < gameController.getPersonagens().Count; i++)
        //{
        //    IInteractable MenorPontoMedio = Tiles[0].GetComponent<IInteractable>();
        //    float menorDistancia = 100;
        //    for (int k = 0; k < Tiles.Count; k++)
        //    {
        //        if (Vector3.Distance(gameController.getPersonagens()[i].transform.position, Tiles[k].transform.position) < menorDistancia)
        //        {
        //            menorDistancia = Vector3.Distance(gameController.getPersonagens()[i].transform.position, Tiles[k].transform.position);
        //            MenorPontoMedio = Tiles[k].GetComponent<IInteractable>();
        //        }
        //    }
        //        MenorPontoMedio.Interagir(gameController.getPersonagens()[i]);
        //}
            
            //RaycastHit raycast;

            //if (Physics.Raycast(gameController.getPersonagens()[i].transform.position, new Vector3(0, -1, 0), out raycast))
            //{
            //    if (raycast.collider.gameObject.GetComponent<IInteractable>() != null)
            //        raycast.collider.gameObject.GetComponent<IInteractable>().Interagir(gameController.getPersonagens()[i]);
            //    Debug.Log(raycast.collider.gameObject.GetComponent<Tile>().Peso);
            //}
            //else
            //{
            //    Debug.Log("Não colidiu");

            //}


        
    //}
}
