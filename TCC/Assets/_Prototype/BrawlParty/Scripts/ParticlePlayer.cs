using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem particle;
    public bool playOnAwake = false;
    public bool loop = false;
    public float duration; 


    private void Start()
    {
        if(playOnAwake)
        {
            if (loop)
                particle.Play();
            else
                StartCoroutine(playParticle(duration));
        }

    }



    public void Play(float duration)
    {
        StartCoroutine(playParticle(duration));        
    }

    IEnumerator playParticle(float sec)
    {
        if (particle != null)
        {
            particle.Play();
            yield return new WaitForSeconds(sec);
            particle.Clear();
            particle.Stop();
        }
    }

}
