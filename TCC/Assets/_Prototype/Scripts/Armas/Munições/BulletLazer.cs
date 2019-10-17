using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLazer : MonoBehaviour
{
    public int damage;
    public float velocidadeDaBala = 3f;
    public Vector3 transformForward;
    public PlayerController player;

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

            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Obstaculo")
        {

            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z + 15*Time.deltaTime);
    }
}
