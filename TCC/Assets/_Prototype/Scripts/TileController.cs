using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
   public Tile tile;

    public bool gambs= false;
    public bool gambs1= false;

    void Start()
    {
        tile = new TileNormal(this.transform);
        InstaciarTile();
    }

    void Update()
    {
        if(gambs)
        {
         tile = new TileNeve(this.transform);
         InstaciarTile();
        }

        if(gambs1)
        {
         tile = new TileBuraco(this.transform);
         InstaciarTile();
        }

    }

    void InstaciarTile()
    {
        if(transform.childCount >0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        Instantiate(tile.Prefab,transform.position, Quaternion.identity,this.transform);

    }


}
