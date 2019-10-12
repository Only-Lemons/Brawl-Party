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
        if (!mirando)
            mirando = true;
    }
    public void MirarNoMaisProximo(int sens)
    {
        try
        {
            Vector3 prox = Vector3.zero;
            for (int i = 0; i < GameManager.Instance.gameController.playerManager.playersControllers.Count; i++)
            {
                for (int j = 0; j < GameManager.Instance.gameController.playerManager.playersControllers.Count; j++)
                {
                    if (i != j)
                    {
                        if (GameManager.Instance.gameController.playerManager.playersControllers[i].transform.position.magnitude != transform.position.magnitude && GameManager.Instance.gameController.playerManager.playersControllers[i].transform.position.magnitude != transform.position.magnitude)
                            if (Vector3.Distance(this.gameObject.transform.position, GameManager.Instance.gameController.playerManager.playersControllers[j].gameObject.transform.position) <= Vector3.Distance(this.gameObject.transform.position, GameManager.Instance.gameController.playerManager.playersControllers[i].gameObject.transform.position))
                            {
                                Debug.Log("Entrei!");
                                Debug.Log("Distancia do i" + Vector3.Distance(this.gameObject.transform.position, GameManager.Instance.gameController.playerManager.playersControllers[i].gameObject.transform.position));
                                Debug.Log("Distancia do j" + Vector3.Distance(this.gameObject.transform.position, GameManager.Instance.gameController.playerManager.playersControllers[j].gameObject.transform.position));

                                prox = GameManager.Instance.gameController.playerManager.playersControllers[i].gameObject.transform.position;
                                transform.LookAt(prox);
                            }
                    }
                }
            }
        }
        catch
        {
            Debug.Log("Procurando players proximos para lookar");
        }
    }
}
