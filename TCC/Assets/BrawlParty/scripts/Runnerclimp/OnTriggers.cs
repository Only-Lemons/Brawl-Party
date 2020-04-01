using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggers : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject.name == "Fogo")
        {
            if (other.tag == "Player")
            {
                Destroy(other.gameObject);
                //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }
        }

        else if(this.gameObject.name == "PlatformBroken(Clone)")
        {
            if (other.tag == "Foot")
                Destroy(this.gameObject, 2.5f);
        }
    }

    private void OnTriggerStay(Collider other) //tratamento de plataformas
    {
        if (this.gameObject.name == "PlatformBroken(Clone)")
        {
            if (other.tag == "Player")
                if (!GetComponent<BoxCollider>().isTrigger)
                    this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        else if (this.gameObject.name == "PlatformFixed(Clone)")
        {
            if (other.tag == "Player")
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other) //tratamento de plataformas
    {
        if (this.gameObject.name == "PlatformBroken(Clone)")
        {
            if (other.tag == "Player")
               this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        else if (this.gameObject.name == "PlatformFixed(Clone)")
        {
            if (other.tag == "Player")
                this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
