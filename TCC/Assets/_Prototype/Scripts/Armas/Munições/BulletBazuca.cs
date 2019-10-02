using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBazuca : MonoBehaviour
{
    public int damage;
    public int velocidadeDaBala = 3;
    public Vector3 transformForward;
    public PlayerController player;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transformForward* 400f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject,5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log(player);
            other.GetComponent<PlayerController>().ReceiveDamage(damage,player);

            for(int i = 0; i<GameManager.Instance.playersPanels.Count; i++)
            {
                if(Vector3.Distance(this.gameObject.transform.position, GameManager.Instance.playersPanels[i].transform.position) < 5)
                {
                    GameManager.Instance.playersPanels[i].GetComponent<PlayerController>().life -= damage/2;
                    Debug.Log("tomou" + GameManager.Instance.playersPanels[i]);
                }
            }
      
            Destroy(this.gameObject);
        }
        else if(other.gameObject.tag == "Obstaculo")
        {
            for(int i = 0; i<GameManager.Instance.playersPanels.Count; i++)
            {
                if(Vector3.Distance(this.gameObject.transform.position, GameManager.Instance.playersPanels[i].transform.position) < 5)
                {
                    GameManager.Instance.playersPanels[i].GetComponent<PlayerController>().life -= damage/2;
                    Debug.Log("tomou" +GameManager.Instance.playersPanels[i]);
                }
            }

            Destroy(this.gameObject);
        }

    }
}
