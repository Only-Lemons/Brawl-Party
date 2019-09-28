using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameMode {
    public GameModes gameMode;
    public Sprite sprite;
}
public enum GameModes
{
    FreeForAll,
    CaptureTheFlag,
    Boss
}
  
 

