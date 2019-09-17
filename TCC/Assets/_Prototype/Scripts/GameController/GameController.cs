using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Singleton;
    float time;
    TileManager tileManager;
    PlayerManager playerManager;
    ArmaManager armaManager;

    void Awake()
    {
        Singleton = this;
        tileManager = GetComponent<TileManager>();
        playerManager = GetComponent<PlayerManager>();
    }

   
}
