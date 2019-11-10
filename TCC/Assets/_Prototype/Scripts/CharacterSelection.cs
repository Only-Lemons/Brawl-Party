using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharacterSelection : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<Image> playersSprite = new List<Image>();
    public GameObject renderPlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addPlayer (List<Image> players) //caso alguém cancele a seleção colocar ele de volta
    {

    } 

    void removePlayer(List<Image> players) //Caso alguém selecione o personagem, tirar ele da lista
    {
    }
}
