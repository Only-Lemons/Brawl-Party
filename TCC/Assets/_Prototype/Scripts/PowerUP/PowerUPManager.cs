using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PowerUpManager
{
    public float time;
    public float tempoAtual;
    public PowerUP PU;
    PlayerController player;
    public GameObject[] Particulas;
   public PowerUpManager(float tempo,PowerUP powerU,PlayerController player)
   {
        this.time = tempo;
        tempoAtual = tempo;
        PU = powerU;
        this.player = player;

   }
   public bool AcabouTempo()
    {
        if (tempoAtual <= 0)
        {
            if (Particulas == null || Particulas.Length > 0 )
                DestruirParticulas();
            return true;
        }
        else
        {
            tempoAtual -= Time.deltaTime;
            PU.Interact(player);
            return false;
        }
    }
    void DestruirParticulas()
    {
        if (Particulas != null)
        {
            for (int i = 0; i < Particulas.Length; i++)
            {
                GameObject.Destroy(Particulas[i]);
            }
        }
    }
}
