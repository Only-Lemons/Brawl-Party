using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player 
{
    int hp;
    float speed;

    public Player(int hp, float speed)
    {
        this.hp = hp;
        this.speed = speed;
    }

    public int Hp { get => hp; set => hp = value; }
    public float Speed { get => speed; set => speed = value; }
}
