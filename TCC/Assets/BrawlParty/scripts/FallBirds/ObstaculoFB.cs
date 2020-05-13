using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoFB : MonoBehaviour
{
    public Vector3
        gravidade,
        forca;

    Vector3
       deslocamento,
       aceleracao,
       velocidade;
        

    public float massa;

    private float tempo;
    // Start is called before the first frame update
    void Start()
    {
        velocidade = Vector3.zero;
        deslocamento = transform.position;
        //gravidade = new Vector3(-1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tempo = Time.fixedDeltaTime;
        aceleracao = (forca / massa) + gravidade;
        velocidade += aceleracao * tempo;
        deslocamento += velocidade * tempo;

        transform.position = deslocamento;
        forca = Vector3.zero;
    }


}
