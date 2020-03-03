using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameMode {
    public GameModes gameMode;
    public Sprite sprite;
    public int Scene;
    public Sprite spriteScene;
}
public enum GameModes
{
    FreeForAll,
    CaptureTheFlag,
    GetItRock,
    JhonBeen,
    SnackAtack,
    RunGhost,
    Boss
}
  
 

