using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreditsPanel : MonoBehaviour
{
    public GameObject creditPanel;

    public void OnCredits()
    {
        Debug.Log("Opa");
        if (!creditPanel.activeInHierarchy)
            creditPanel.SetActive(true);
        else
            creditPanel.SetActive(false);
    }
    
}
