using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public int distancia = 10;
    public int sensibilidade;
    void Start()
    {

    }

    void Update()
    {
        MirarNoMaisProximo(sensibilidade);
    }

    void MirarNoMaisProximo(int sens)
    {
        Vector3 prox = Vector3.zero;

        for (int i = 0; i < GameManager.Instance.gameController.playerManager.Players.Count; i++)
        {
            for (int j = 0; j < GameManager.Instance.gameController.playerManager.Players.Count; j++)
            {
                if (i != j)
                {
                    prox = GameManager.Instance.gameController.playerManager.Players[j].transform.position;
                    if (Vector3.Distance(GameManager.Instance.gameController.playerManager.Players[i].transform.position, prox) < distancia)
                    {
                        GameManager.Instance.gameController.playerManager.Players[i].transform.LookAt(prox);
                    }
                }
            }
        }
    }
}
