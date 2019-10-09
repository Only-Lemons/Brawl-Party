using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAim : MonoBehaviour
{
    public int distancia = 15;
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
        try
        {
            for (int i = 0; i < GameManager.Instance.gameController.playerManager.Players.Count; i++)
            {
                Transform prox = GameManager.Instance.gameController.playerManager.Players[i].gameObject.transform;

                if (Vector3.Distance(transform.position, prox.position) > distancia)
                {
                    transform.LookAt(prox);
                    //GameManager.Instance.gameController.playerManager.Players[i].gameObject.GetComponent<Rigidbody>().MoveRotation(Quaternion.RotateTowards(iRotation, jRotation, 2 * Time.deltaTime));
                    //Quaternion targetRotation = Quaternion.LookRotation(GameManager.Instance.gameController.playerManager.Players[j].transform.position);
                    //GameManager.Instance.gameController.playerManager.Players[i].gameObject.transform.rotation = Quaternion.Lerp(targetRotation, iRotation, 0.5f*Time.deltaTime);
                }
            }
        }
        catch
        {
            Debug.Log("Procurando players proximos para lookar");
        }
    }
}
