using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSelectManager : MonoBehaviour
{
    public PlayerInputManager player;



    private void Awake()
    {
        player = GetComponent<PlayerInputManager>();
    }

    private void Update()
    {
        player.playerPrefab.transform.SetParent(this.transform);
    }


}
