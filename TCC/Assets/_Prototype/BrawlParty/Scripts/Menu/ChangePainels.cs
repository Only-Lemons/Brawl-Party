using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangePainels : MonoBehaviour
{
    public List<GameObject> Painels = new List<GameObject>();
    void Start(){
        
        Painels[0].SetActive(true);
        for (int i = 1; i < Painels.Count; i++)
        {
            Painels[i].SetActive(false);
        }
    }
    public void ChangePainel(int Index)
    {
        for (int i = 0; i < Painels.Count; i++)
        {
            if (i == Index) {
                Painels[i].SetActive(true);
            }
            else
            {
                Painels[i].SetActive(false);
            }
        }
    }
}
