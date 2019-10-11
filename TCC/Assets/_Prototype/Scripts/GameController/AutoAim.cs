using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public int distancia = 15;
    public int sensibilidade;
    public bool mirando = false;

    void Start()
    {
        mirando = false;
    }
    void Update()
    {
        if (mirando)
            MirarNoMaisProximo(sensibilidade);
    }

    public void SetarBool()
    {
        if(!mirando)
            mirando = true;
    }
    public void MirarNoMaisProximo(int sens)
    {
        try
        {
            Vector3 prox = Vector3.zero;
            for (int i = 0; i < GameManager.Instance.gameController.playerManager.playersControllers.Count; i++)
            {
                //if (Vector3.Distance(transform.position, GameManager.Instance.gameController.playerManager.Players[i].gameObject.transform.position) < 5)
                {
                    prox = GameManager.Instance.gameController.playerManager.playersControllers[i].gameObject.transform.position;
                }
                transform.LookAt(prox);
            }
        }
        catch
        {
            Debug.Log("Procurando players proximos para lookar");
        }
    }
}
