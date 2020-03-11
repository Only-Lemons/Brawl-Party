using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myRB : MonoBehaviour
{
    /*public*/ Vector3
        forca;

    private Vector3
        deslocamento,
        aceleracao,
        velocidade,
        gravidade;

    public float massa;
        
        

    private float tempo;

    void Start()
    {
        velocidade = Vector3.zero;
        deslocamento = transform.position;
        gravidade = new Vector3(0, -9.81f, 0);
    }

    void FixedUpdate()
    {
        tempo = Time.fixedDeltaTime;
        aceleracao = (forca / massa) + gravidade;
        velocidade += aceleracao * tempo;
        deslocamento += velocidade * tempo;

        if(deslocamento.y <= 0.5)
        {
            velocidade = velocidade.magnitude * Reflexao(velocidade.normalized, Vector3.up);
            deslocamento.y = 0.5f;
        }

        if (velocidade.magnitude > 0.1f)
            transform.position = deslocamento;

        //transform.position = deslocamento;
        forca = Vector3.zero;
    }

    public static Vector3 Reflexao(Vector3 vetor, Vector3 normal)
    {
        return vetor - 2 * Vector3.Dot(vetor, normal) * normal;
    }

    public void AddForce(Vector3 x, float f)
    {
        forca = SubVec3(transform.position, x) * f;
    }

    Vector3 SubVec3(Vector3 a, Vector3 b)
    {
        Vector3 x = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z).normalized;
        return x;
    }
}