using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBujing : MonoBehaviour
{
    public int damage;
    public int velocidadeDaBala = 3;
    public Vector3 transformForward;
    public PlayerController player;
    AudioSource source;
    public AudioClip audio;
    bool virar;
    bool iniciar;
    private float cont;


    void Start()
    {
        virar = false;
        iniciar = false;
        cont = 0;
        source = GetComponent<AudioSource>();
        source.clip = audio;
        GetComponent<Rigidbody>().AddForce(transformForward * 600f * velocidadeDaBala, ForceMode.Acceleration);
        Destroy(this.gameObject, 5f);
    }

    private void Update()
    {
        if (iniciar)
            Contador();
    }

    void Contador()
    {
        cont += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        source.Play();
        if (other.gameObject.tag == "Player")
        {
            Debug.Log(player);
            other.GetComponent<PlayerController>().ReceiveDamage(damage, player);

            Destroy(this.gameObject);
        }
        else if (other.gameObject.tag == "Obstaculo")
        {
            if (virar == false)
            {
                GetComponent<Rigidbody>().AddForce(-transformForward * 1200f * velocidadeDaBala, ForceMode.Acceleration);
            }

            virar = true;
            iniciar = true;

            if (cont > 1.3f)
            {

                Destroy(this.gameObject);
            }
        }

    }
}
