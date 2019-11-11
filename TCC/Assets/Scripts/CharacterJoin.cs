using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterJoin : MonoBehaviour
{
    //public GameObject[] panel = new GameObject[4];
    //public GameObject[] playerPanel = new GameObject[4];

    //public RectTransform canvas;
    //private int playersActive = 0;

    public GameObject player;
    public GameObject[] slots = new GameObject[4];
    [HideInInspector] public int playerInScene = 0;

    public void OnPlayerJoined()
    {

    }

    public void OnPlayerLeft()
    {

    }
}
