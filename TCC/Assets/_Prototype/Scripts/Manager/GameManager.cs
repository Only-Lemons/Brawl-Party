using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameController sceneController;
    public GameObject playerInputPrefab;
    public List<GameObject> playerInputsPrefabs;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
