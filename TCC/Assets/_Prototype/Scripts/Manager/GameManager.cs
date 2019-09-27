using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameController sceneController;
    public GameObject playerInputPrefab;
    public List<GameObject> playerInputsPrefabs;

    #region LevelInteract
    public int nextLevel;
    #endregion
    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }      
    }

}
