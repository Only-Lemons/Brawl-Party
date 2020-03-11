using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    Vector3 nextFence;
    bool inJump = false;
    private void Update()
    {
        if (!inJump)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, nextFence, 1);
        }
    }
    public void Moviment(Vector3 postion)
    {
        nextFence = new Vector3(postion.x, postion.y-2, postion.z);
    }
    public void Jump()
    {
        if (!inJump)
        {
           // this.transform.position = Vector3
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Destroied")
            GameObject.Destroy(other.gameObject);
        PlayerController aux = other.GetComponent<PlayerController>();
        if (aux != null)
        {
            aux.actualGameMode.HitRule(aux);
        }
            
    }

}
