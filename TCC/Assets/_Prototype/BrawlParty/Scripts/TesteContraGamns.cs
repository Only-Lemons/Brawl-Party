using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteContraGamns : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        //for (int i = 0; i < GameManager.Instance.gameController.playerManager.playersControllers.Count; i++)
        //{
        //    GameManager.Instance.gameController.playerManager.playersControllers[i].gameObject.SetActive(true);
        //}

        for (int i = 0; i < GameManager.Instance.playersPanels.Count; i++)
        {
            if(GameManager.Instance.playersPanels[i].transform.childCount > 0)
                Destroy(GameManager.Instance.playersPanels[i].transform.GetChild(0).gameObject);
        }
    }

}
