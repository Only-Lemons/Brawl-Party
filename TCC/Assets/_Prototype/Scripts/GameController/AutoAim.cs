using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public int distancia = 15;
    public int sensibilidade;

    [HideInInspector]
    public bool mirando = false;

    [HideInInspector]
    public Quaternion mirandoRotacao;

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
            foreach (PlayerController um in GameManager.Instance.gameController.playerManager.playersControllers)
            {
                foreach (PlayerController outro in GameManager.Instance.gameController.playerManager.playersControllers)
                {
                    {
                        if (outro.canDeath)
                            if (Vector3.Distance(this.transform.position, outro.transform.position) <= Vector3.Distance(this.transform.position, um.transform.position))
                            {
                                prox = outro.transform.position;
                                transform.LookAt(prox);
                                mirandoRotacao = transform.rotation; //controle de rotação atual
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
