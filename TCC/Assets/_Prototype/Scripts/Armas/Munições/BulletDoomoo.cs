using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDoomoo : MonoBehaviour
{
    public int damage = 1;
    public float velocidadeDaBala = 1.5f;
    public Vector3 transformForward;
    public PlayerController player;
    AudioSource source;
    public AudioClip audio;
    GameObject doomooEfeito;

    void Start()
    {
        doomooEfeito = Resources.Load("Municoes/Efeitos/DoomooEfeito") as GameObject;
        source = GetComponent<AudioSource>();
        source.clip = audio;
        GetComponent<Rigidbody>().AddForce(transformForward * 400f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject, 5f);

    }

    private void OnTriggerEnter(Collider other)
    {
        source.Play();
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(player);
            other.GetComponent<PlayerController>().ReceiveDamage(damage, player);

            Instantiate(doomooEfeito, this.transform.position, Quaternion.identity, GameObject.Find("GameManager").transform);

            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Obstaculo")
        {
            Instantiate(doomooEfeito, this.transform.position, Quaternion.identity, GameObject.Find("GameManager").transform);

            Destroy(this.gameObject);
        }

        

    }
}
