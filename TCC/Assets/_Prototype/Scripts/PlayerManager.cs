using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    List<Player> Players;
    public int NumPlayer = 0;


    // Start is called before the first frame update
    void Start()
    {
      
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
