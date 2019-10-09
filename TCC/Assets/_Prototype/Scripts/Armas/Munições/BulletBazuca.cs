using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBazuca : MonoBehaviour
{
    public int damage;
    public float velocidadeDaBala = 1.5f;
    public Vector3 transformForward;
    public PlayerController player;

    public GameObject testExplosao;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transformForward * 400f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject, 5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(player);
            other.GetComponent<PlayerController>().ReceiveDamage(damage, player);

            Instantiate(testExplosao, this.transform.position, Quaternion.identity, GameObject.Find("GameManager").transform);

            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Obstaculo")
        {
            Instantiate(testExplosao, this.transform.position, Quaternion.identity, GameObject.Find("GameManager").transform);

            Destroy(this.gameObject);
        }

    }
}
