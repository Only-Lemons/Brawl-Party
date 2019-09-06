using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu (fileName ="Personagens",menuName = "Player")]
public class SOPlayer : ScriptableObject
{
   public string nome;
   public int hp;
   public int actualHP;
   public float speed;
   public GameObject prefab;
   public Animator stateAnimation;
    
  
}
