using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTeletiroporte : MonoBehaviour
{
    public int damage;
    public float velocidadeDaBala = 1.5f;
    public Vector3 transformForward;
    public PlayerController player;

    GameObject teletiroportador;

    void Start()
    {
        teletiroportador = Resources.Load("Municoes/Efeitos/Teletiroportador") as GameObject;
        GetComponent<Rigidbody>().AddForce(transformForward * 400f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject, 5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstaculo")
        {
            Instantiate(teletiroportador, this.transform.position, Quaternion.identity, GameObject.Find("GameManager").transform);

            Destroy(this.gameObject);
        }
    }
}
