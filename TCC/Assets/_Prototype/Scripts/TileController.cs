using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public Tile tile;

    void Start()
    {
        //tile = new TileNormal(this.transform);
        InstaciarTile();
    }

    

    public void InstaciarTile()
    {
        if(transform.childCount>0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        Instantiate(tile.Prefab, transform.position, Quaternion.identity,this.transform);

    }


}
