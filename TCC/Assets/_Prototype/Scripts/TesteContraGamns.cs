using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteContraGamns : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < GameManager.Instance.gameController.playerManager.playersControllers.Count; i++)
        {
            GameManager.Instance.gameController.playerManager.playersControllers[i].gameObject.SetActive(true);
        }
    }

}
