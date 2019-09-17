using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public static TerrainController instance;


   public List<Tile> tilesInstanciados = new List<Tile>();
   
 

    void Awake()
    {
        instance = this;
    }



    private void Start()
    {    
       
    }


   
}
