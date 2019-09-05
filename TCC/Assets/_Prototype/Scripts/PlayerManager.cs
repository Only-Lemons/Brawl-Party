using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerController> Players = new List<PlayerController>();
    int NumPlayer = 0;
    public static PlayerManager instance;


    // Start is called before the first frame update
    void Start()
    {
        getPlayerInScene();
       
    }
    void getPlayerInScene()
    {
        GameObject[] aux;
        aux = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject item in aux)
        {
            Players.Add(item.GetComponent<PlayerController>());
        }
    }
    void GerenciarInputs()
    {
        InputSystem.onDeviceChange +=
    (device, change) =>
    {
        switch (change)
        {
            case InputDeviceChange.Added:
                AdicionarNovoPlayer();
                break;

            case InputDeviceChange.Removed:
                NumPlayer--;
                break;
        }
    };
    }

    void AdicionarNovoPlayer()
    {
        NumPlayer++;
        
    }
    // Update is called once per frame
    void Update()
    {
        GerenciarInputs();
    }
}
