using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
   
    [SerializeField]
    int tamanho = 100;
    [SerializeField]
    int pesoTotal = 0;
    [SerializeField]
    int pesoMaximo = 100;
    [SerializeField]
    GameObject tile;
    GameObject anterior = null;
    GameObject primeiroFila = null; 
    bool negativeZ = false;
    Vector3 offsetx = new Vector3(1.73f,0,0);
    Vector3 offsetz = new Vector3(0.86f,0,1.5f);
    Vector3 offsetzN = new Vector3(-0.86f,0,1.5f);

    void Start()
    {
        Instanciar();
    }



   void Instanciar()
   {
        
       for (int i = 0; i < tamanho; i++)
       {
           
            GameObject tileClone = Instantiate(tile);

            int rnd = Random.Range(0,10);
            
            if(rnd>7)
            {
                int rnd2 = Random.Range(0,10);
                if(rnd2 <5)
                {
                    tileClone.GetComponent<TileController>().tile = new TileGelo(tileClone.transform);
                }
                else if( rnd2 >= 5 && rnd2 < 8)
                {
                     tileClone.GetComponent<TileController>().tile = new TileNeve(tileClone.transform);
                }
                else
                {
                     tileClone.GetComponent<TileController>().tile = new TileBuraco(tileClone.transform);
                }

            }
            else
            {
                tileClone.GetComponent<TileController>().tile = new TileNormal(tileClone.transform);
            }

            if(anterior == null)
            {
                anterior = tileClone;
                primeiroFila = tileClone;
                tileClone.transform.position = Vector3.zero;//provisorio
            }
            else
            {
                if(i%Mathf.Sqrt(tamanho) == 0)
                {
                    if(!negativeZ)
                    {
                        tileClone.transform.position = offsetz + primeiroFila.transform.position;
                        negativeZ = true;
                    }
                    else
                    {
                        negativeZ = false;
                        tileClone.transform.position = offsetzN + primeiroFila.transform.position;
                    }
                    
                    primeiroFila = tileClone;
                }
                else
                {
                    tileClone.transform.position = offsetx + anterior.transform.position;
                }
                anterior = tileClone;
            } 

            pesoTotal += tileClone.GetComponent<TileController>().tile.Peso;
            TerrainController.instance.tilesInstanciados.Add(tileClone.GetComponent<TileController>());

       }
   }

}
