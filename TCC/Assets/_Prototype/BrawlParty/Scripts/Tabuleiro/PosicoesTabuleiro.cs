using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosicoesTabuleiro : MonoBehaviour
{
    public int id;

    public int ouroB;
    public int velMovimentoB;
    public int escudoB;

    bool recebeuBonus;

    Renderer efeitoBonusParticula; //provisorio
    Vector3 efeito; //provisorio
    Color novaCor; //provisorio test

    private void Start()
    {
        efeitoBonusParticula = gameObject.GetComponent<Renderer>();
        efeitoBonusParticula.material = new Material(gameObject.GetComponent<Shader>());

        
        
        recebeuBonus = false;
    }

    public void ReceberBonus(PlayerController player)
    {
        if (!recebeuBonus)
        {
            player.speed += velMovimentoB;
            player.shield += escudoB;

            //player.gold += ouroB;

            novaCor = new Color(1, 0, 0, 0.5f);
            efeito = new Vector3(gameObject.transform.localScale.x / 2, gameObject.transform.localScale.y / 2, gameObject.transform.localScale.z / 2);

            recebeuBonus = true;
        }
    }

    private void Update()
    {
        if (recebeuBonus)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, efeito, Time.deltaTime);
            efeitoBonusParticula.material.color = Vector4.Lerp(efeitoBonusParticula.material.color, novaCor, Time.deltaTime);
        }
    }
}
