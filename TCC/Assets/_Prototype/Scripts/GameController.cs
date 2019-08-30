using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Personagem> Personagens = new List<Personagem>();

    public List<Personagem> getPersonagens()
    {
        return Personagens;
    }
 
}
