using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public List<Particulas> particulas = new List<Particulas>();
    
    Vector3 _positionPattern = new Vector3(-1000,1000,-1000);
    public void getParticula(string name, Transform position)
    {
        Particulas aux = particulas.Find(x => x.nome.ToLower() == name.ToLower());
        GameObject particula = Instantiate(aux.particle, position.position, Quaternion.identity,position);
        StartCoroutine(resetParticle(aux,particula));
        particulas.Remove(aux);


    }


    public void getParticulaUI(string name, RectTransform position)
    {
        Particulas aux = particulas.Find(x => x.nome.ToLower() == name.ToLower());
        aux.particle.gameObject.transform.parent = position;
        aux.particle.gameObject.transform.position = aux.posicaoRelativa;
        GameObject particula = Instantiate(aux.particle, position.position, Quaternion.identity,position);
        StartCoroutine(resetParticle(aux,particula));
        particulas.Remove(aux);

    }


    IEnumerator resetParticle(Particulas particles,GameObject particula)
    {
        yield return new WaitForSeconds(particles.tempo);
        particulas.Add(particles);
        GameObject.Destroy(particula);
    }
}
[System.Serializable]       
public struct Particulas
{
    public string nome;
    public GameObject particle;
    public float tempo;
    public Vector3 posicaoRelativa;
}