using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
   public static TerrainController instance;


    public List<Tile> tilesInstanciados = new List<Tile>();
    public List<Vector3> bases = new List<Vector3>();


    void Awake()
    {
        instance = this;

    }


    


   
}
