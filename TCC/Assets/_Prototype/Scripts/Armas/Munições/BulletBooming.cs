using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBooming : MonoBehaviour
{
    public int damage;
    public int velocidadeDaBala = 3;
    public Vector3 transformForward;
    public PlayerController player;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transformForward * 300f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject, 5f);
    }

    private void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + (Time.deltaTime*3), transform.localScale.x, transform.localScale.x);
        damage += (int)(Time.deltaTime*10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(player);
            other.GetComponent<PlayerController>().ReceiveDamage(damage, player);
            //int random = Random.Range(0, 3);
            //player.transform.position = new Vector3(transform.position.x + random, transform.position.y, transform.position.z + random);

            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Obstaculo")
        {
            Destroy(this.gameObject);
        }

    }
}
