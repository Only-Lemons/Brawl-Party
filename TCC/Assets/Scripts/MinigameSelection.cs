using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameSelection : MonoBehaviour
{

    private int idPanel = 0;
    public List<GameObject> panels = new List<GameObject>();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("01_MainMenu");
    }

    public void changeIdUp()
    {
        if(idPanel < panels.Count-1)
        {
           idPanel++;
           panels[idPanel].SetActive(true);
           panels[idPanel - 1].SetActive(false);
        }
          
    }
    public void changeIdDown()
    {
        if(idPanel > 0) 
        {
           idPanel--;
           panels[idPanel].SetActive(true);
           panels[idPanel + 1].SetActive(false);
        }
    }
}
